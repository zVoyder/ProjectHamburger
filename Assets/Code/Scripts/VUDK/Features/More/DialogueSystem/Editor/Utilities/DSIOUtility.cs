namespace VUDK.Features.More.DialogueSystem.Editor.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEditor;
    using UnityEditor.Experimental.GraphView;
    using UnityEngine;
    using VUDK.Extensions;
    using VUDK.Features.More.DialogueSystem.Data;
    using VUDK.Features.More.DialogueSystem.Editor.Constants;
    using VUDK.Features.More.DialogueSystem.Editor.Data.Save;
    using VUDK.Features.More.DialogueSystem.Editor.Elements;
    using VUDK.Features.More.DialogueSystem.Editor.Windows;
    using VUDK.Features.More.DialogueSystem.Utility;
    using VUDK.Generic.Serializable;
    using static VUDK.Features.More.DialogueSystem.Editor.Constants.DSEditorPaths;

    public static class DSIOUtility
    {
        private static DSGraphView s_graphView;
        private static string s_graphFileName;
        private static string s_containerFolderPath;

        private static List<DSNode> s_nodes;
        private static List<DSGroup> s_groups;

        private static Dictionary<string, DSDialogueGroupData> s_createdDialogueGroups;
        private static Dictionary<string, DSDialogueData> s_createdDialogues;

        private static Dictionary<string, DSGroup> s_loadedGroups;
        private static Dictionary<string, DSNode> s_loadedNodes;

        public static void Init(DSGraphView dsGraphView, string graphName)
        {
            s_graphView = dsGraphView;

            s_graphFileName = graphName;
            s_containerFolderPath = $"{DialoguesDataFolderPath}/{graphName}";

            s_nodes = new List<DSNode>();
            s_groups = new List<DSGroup>();

            s_createdDialogueGroups = new Dictionary<string, DSDialogueGroupData>();
            s_createdDialogues = new Dictionary<string, DSDialogueData>();

            s_loadedGroups = new Dictionary<string, DSGroup>();
            s_loadedNodes = new Dictionary<string, DSNode>();
        }

        public static void Save()
        {
            CreateFoldersForSingleDialogue();

            GetElementsFromGraphView();

            DSGraphEditorData graphData = CreateAsset<DSGraphEditorData>(GraphsAssetPath, $"{s_graphFileName}Graph");

            graphData.Init(s_graphFileName);

            DSDialogueContainerData dialogueContainer = CreateAsset<DSDialogueContainerData>(s_containerFolderPath, s_graphFileName);

            dialogueContainer.Init(s_graphFileName);

            SaveGroups(graphData, dialogueContainer);
            SaveNodes(graphData, dialogueContainer);

            SaveAsset(graphData);
            SaveAsset(dialogueContainer);
        }

        private static void SaveGroups(DSGraphEditorData graphData, DSDialogueContainerData dialogueContainer)
        {
            List<string> groupNames = new List<string>();

            foreach (DSGroup group in s_groups)
            {
                SaveGroupToGraph(group, graphData);
                SaveGroupToScriptableObject(group, dialogueContainer);

                groupNames.Add(group.title);
            }

            UpdateOldGroups(groupNames, graphData);
        }

        private static void SaveGroupToGraph(DSGroup group, DSGraphEditorData graphData)
        {
            DSGroupEditorData groupData = new DSGroupEditorData()
            {
                GroupID = group.GroupID,
                Name = group.title,
                Position = group.GetPosition().position
            };

            graphData.Groups.Add(groupData);
        }

        private static void SaveGroupToScriptableObject(DSGroup group, DSDialogueContainerData dialogueContainer)
        {
            string groupName = group.title;

            CreateFolder($"{s_containerFolderPath}/Groups", groupName);
            CreateFolder($"{s_containerFolderPath}/Groups/{groupName}", DialoguesNodesFolderName);

            DSDialogueGroupData dialogueGroup = CreateAsset<DSDialogueGroupData>($"{s_containerFolderPath}/Groups/{groupName}", groupName);

            dialogueGroup.Init(groupName);

            s_createdDialogueGroups.Add(group.GroupID, dialogueGroup);

            dialogueContainer.DialogueGroups.Add(dialogueGroup, new List<DSDialogueData>());

            SaveAsset(dialogueGroup);
        }

        private static void UpdateOldGroups(List<string> currentGroupNames, DSGraphEditorData graphData)
        {
            if (graphData.OldGroupNames != null && graphData.OldGroupNames.Count != 0)
            {
                List<string> groupsToRemove = graphData.OldGroupNames.Except(currentGroupNames).ToList();

                foreach (string groupToRemove in groupsToRemove)
                {
                    RemoveFolder($"{s_containerFolderPath}/Groups/{groupToRemove}");
                }
            }

            graphData.OldGroupNames = new List<string>(currentGroupNames);
        }

        private static void SaveNodes(DSGraphEditorData graphData, DSDialogueContainerData dialogueContainer)
        {
            SerializableDictionary<string, List<string>> groupedNodeNames = new SerializableDictionary<string, List<string>>();
            List<string> ungroupedNodeNames = new List<string>();

            foreach (DSNode node in s_nodes)
            {
                SaveNodeToGraph(node, graphData);
                SaveNodeToScriptableObject(node, dialogueContainer);

                if (node.Group != null)
                {
                    groupedNodeNames.AddItem(node.Group.title, node.DialogueName);
                    continue;
                }

                ungroupedNodeNames.Add(node.DialogueName);
            }

            UpdateDialoguesChoicesConnections();
            UpdateOldGroupedNodes(groupedNodeNames, graphData);
            UpdateOldUngroupedNodes(ungroupedNodeNames, graphData);
        }

        private static void SaveNodeToGraph(DSNode node, DSGraphEditorData graphData)
        {
            List<DSChoiceEditorData> choices = CloneNodeChoices(node.Choices);

            DSNodeEditorData nodeData = new DSNodeEditorData()
            {
                NodeID = node.NodeID,
                Name = node.DialogueName,
                Choices = choices,
                DialogueText = node.DialogueText,
                ActorData = node.ActorData,
                DialogueAudioClip = node.DialogueAudioClip,
                GroupID = node.Group?.GroupID,
                DialogueType = node.DialogueType,
                Position = node.GetPosition().position
            };

            graphData.Nodes.Add(nodeData);
        }

        private static void SaveNodeToScriptableObject(DSNode node, DSDialogueContainerData dialogueContainer)
        {
            DSDialogueData dialogue;

            if (node.Group != null)
            {
                dialogue = CreateAsset<DSDialogueData>($"{s_containerFolderPath}/Groups/{node.Group.title}/{DialoguesNodesFolderName}", node.DialogueName);
                dialogueContainer.DialogueGroups.AddItem(s_createdDialogueGroups[node.Group.GroupID], dialogue);
            }
            else
            {
                dialogue = CreateAsset<DSDialogueData>($"{s_containerFolderPath}/Global/{DialoguesNodesFolderName}", node.DialogueName);
                dialogueContainer.UngroupedDialogues.Add(dialogue);

                if (node.IsStartNode())
                    dialogueContainer.StartingDialogues.Add(dialogue);
            }

            dialogue.Init(
                node.DialogueName,
                node.ActorData,
                node.DialogueAudioClip,
                node.DialogueText,
                ConvertNodeChoicesToDialogueChoices(node.Choices),
                node.DialogueType,
                node.IsStartNode()
            );

            s_createdDialogues.Add(node.NodeID, dialogue);

            SaveAsset(dialogue);
        }

        private static List<DSDialogueChoiceData> ConvertNodeChoicesToDialogueChoices(List<DSChoiceEditorData> nodeChoices)
        {
            List<DSDialogueChoiceData> dialogueChoices = new List<DSDialogueChoiceData>();

            foreach (DSChoiceEditorData nodeChoice in nodeChoices)
            {
                DSDialogueChoiceData choiceData = new DSDialogueChoiceData()
                {
                    Text = nodeChoice.Text
                };

                dialogueChoices.Add(choiceData);
            }

            return dialogueChoices;
        }

        private static void UpdateDialoguesChoicesConnections()
        {
            foreach (DSNode node in s_nodes)
            {
                DSDialogueData dialogue = s_createdDialogues[node.NodeID];

                for (int choiceIndex = 0; choiceIndex < node.Choices.Count; choiceIndex++)
                {
                    DSChoiceEditorData nodeChoice = node.Choices[choiceIndex];

                    if (string.IsNullOrEmpty(nodeChoice.NodeID))
                        continue;

                    dialogue.Choices[choiceIndex].NextDialogue = s_createdDialogues[nodeChoice.NodeID];
                    SaveAsset(dialogue);
                }
            }
        }

        private static void UpdateOldGroupedNodes(SerializableDictionary<string, List<string>> currentGroupedNodeNames, DSGraphEditorData graphData)
        {
            if (graphData.OldGroupedNodeNames != null && graphData.OldGroupedNodeNames.Count != 0)
            {
                foreach (KeyValuePair<string, List<string>> oldGroupedNode in graphData.OldGroupedNodeNames)
                {
                    List<string> nodesToRemove = new List<string>();

                    if (currentGroupedNodeNames.ContainsKey(oldGroupedNode.Key))
                    {
                        nodesToRemove = oldGroupedNode.Value.Except(currentGroupedNodeNames[oldGroupedNode.Key]).ToList();
                    }

                    foreach (string nodeToRemove in nodesToRemove)
                    {
                        RemoveAsset($"{s_containerFolderPath}/Groups/{oldGroupedNode.Key}/{DialoguesNodesFolderName}", nodeToRemove);
                    }
                }
            }

            graphData.OldGroupedNodeNames = new SerializableDictionary<string, List<string>>(currentGroupedNodeNames);
        }

        private static void UpdateOldUngroupedNodes(List<string> currentUngroupedNodeNames, DSGraphEditorData graphData)
        {
            if (graphData.OldUngroupedNodeNames != null && graphData.OldUngroupedNodeNames.Count != 0)
            {
                List<string> nodesToRemove = graphData.OldUngroupedNodeNames.Except(currentUngroupedNodeNames).ToList();

                foreach (string nodeToRemove in nodesToRemove)
                {
                    RemoveAsset($"{s_containerFolderPath}/Global/{DialoguesNodesFolderName}", nodeToRemove);
                }
            }

            graphData.OldUngroupedNodeNames = new List<string>(currentUngroupedNodeNames);
        }

        public static Texture2D LoadIcon(string path)
        {
            return EditorGUIUtility.Load($"{EditorIconsPath}/{path}") as Texture2D;
        }

        public static void Load()
        {
            DSGraphEditorData graphData = LoadAsset<DSGraphEditorData>(GraphsAssetPath, s_graphFileName);

            if (graphData == null)
            {
                EditorUtility.DisplayDialog(
                    "Could not find the file!",
                    "The file at the following path could not be found:\n\n" +
                    $"\"{GraphsAssetPath}/{s_graphFileName}\".\n\n" +
                    "Make sure you chose the right file and it's placed at the folder path mentioned above.",
                    "Thanks!"
                );

                return;
            }

            DSEditorWindow.UpdateFileName(graphData.FileName);

            LoadGroups(graphData.Groups);
            LoadNodes(graphData.Nodes);
            LoadNodesConnections();
        }

        private static void LoadGroups(List<DSGroupEditorData> groups)
        {
            foreach (DSGroupEditorData groupData in groups)
            {
                DSGroup group = s_graphView.CreateGroup(groupData.Name, groupData.Position);

                group.GroupID = groupData.GroupID;

                s_loadedGroups.Add(group.GroupID, group);
            }
        }

        private static void LoadNodes(List<DSNodeEditorData> nodes)
        {
            foreach (DSNodeEditorData nodeSaveData in nodes)
            {
                List<DSChoiceEditorData> choices = CloneNodeChoices(nodeSaveData.Choices);

                DSNode node = s_graphView.CreateNode(nodeSaveData.Name, nodeSaveData.DialogueType, nodeSaveData.Position, false);

                node.NodeID = nodeSaveData.NodeID;
                node.Choices = choices;
                node.DialogueText = nodeSaveData.DialogueText;
                node.ActorData = nodeSaveData.ActorData;
                node.DialogueAudioClip = nodeSaveData.DialogueAudioClip;
                node.Draw();

                s_graphView.AddElement(node);

                s_loadedNodes.Add(node.NodeID, node);

                if (string.IsNullOrEmpty(nodeSaveData.GroupID))
                {
                    continue;
                }

                DSGroup group = s_loadedGroups[nodeSaveData.GroupID];

                node.Group = group;

                group.AddElement(node);
            }
        }

        private static void LoadNodesConnections()
        {
            foreach (KeyValuePair<string, DSNode> loadedNode in s_loadedNodes)
            {
                foreach (Port choicePort in loadedNode.Value.outputContainer.Children())
                {
                    DSChoiceEditorData choiceData = (DSChoiceEditorData)choicePort.userData;

                    if (string.IsNullOrEmpty(choiceData.NodeID))
                    {
                        continue;
                    }

                    DSNode nextNode = s_loadedNodes[choiceData.NodeID];

                    Port nextNodeInputPort = (Port)nextNode.inputContainer.Children().First();

                    Edge edge = choicePort.ConnectTo(nextNodeInputPort);

                    s_graphView.AddElement(edge);

                    loadedNode.Value.RefreshPorts();
                }
            }
        }

        private static void GetElementsFromGraphView()
        {
            s_graphView.graphElements.ForEach(graphElement =>
            {
                if (graphElement is DSNode node)
                {
                    s_nodes.Add(node);
                    return;
                }

                if (graphElement is DSGroup group)
                {
                    s_groups.Add(group);
                    return;
                }
            });
        }

        public static void CreateMainFolders()
        {
            CreateFolder(EditorFolderPath, "Graphs");
            CreateFolder(DialoguesSaveParentFolderPath, DialoguesDataFolderName);
            CreateFolder($"{DialoguesSaveParentFolderPath}/{DialoguesDataFolderName}", "AllDialogues");
            CreateFolder($"{DialoguesSaveParentFolderPath}/{DialoguesDataFolderName}", "Actors");
        }

        private static void CreateFoldersForSingleDialogue()
        {
            CreateFolder(DialoguesDataFolderPath, s_graphFileName);
            CreateFolder(s_containerFolderPath, "Global");
            CreateFolder(s_containerFolderPath, "Groups");
            CreateFolder($"{s_containerFolderPath}/Global", DialoguesNodesFolderName);
        }

        public static void CreateFolder(string parentFolderPath, string newFolderName)
        {
            if (AssetDatabase.IsValidFolder($"{parentFolderPath}/{newFolderName}"))
            {
                return;
            }

            AssetDatabase.CreateFolder(parentFolderPath, newFolderName);
        }

        public static void RemoveFolder(string path)
        {
            FileUtil.DeleteFileOrDirectory($"{path}.meta");
            FileUtil.DeleteFileOrDirectory($"{path}/");
        }

        public static void CreateActorAsset(Sprite actorIcon, string actorName)
        {
            string id = Guid.NewGuid().ToString().Substring(0, 4);
            DSActorData actor = CreateAsset<DSActorData>($"{DialoguesActorsAssetPath}", $"actor_{actorName.RemoveWhitespaces()}_{id}");
            actor.Init(actorIcon, actorName);
            SaveAsset(actor);
        }

        public static T CreateAsset<T>(string path, string assetName) where T : ScriptableObject
        {
            string fullPath = $"{path}/{assetName}.asset";

            T asset = LoadAsset<T>(path, assetName);

            if (asset == null)
            {
                asset = ScriptableObject.CreateInstance<T>();
                AssetDatabase.CreateAsset(asset, fullPath);
            }

            return asset;
        }

        public static T LoadAsset<T>(string path, string assetName) where T : ScriptableObject
        {
            string fullPath = $"{path}/{assetName}.asset";

            return AssetDatabase.LoadAssetAtPath<T>(fullPath);
        }

        public static void SaveAsset(UnityEngine.Object asset)
        {
            EditorUtility.SetDirty(asset);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public static void RemoveAsset(string path, string assetName)
        {
            AssetDatabase.DeleteAsset($"{path}/{assetName}.asset");
        }

        private static List<DSChoiceEditorData> CloneNodeChoices(List<DSChoiceEditorData> nodeChoices)
        {
            List<DSChoiceEditorData> choices = new List<DSChoiceEditorData>();

            foreach (DSChoiceEditorData choice in nodeChoices)
            {
                DSChoiceEditorData choiceData = new DSChoiceEditorData()
                {
                    Text = choice.Text,
                    NodeID = choice.NodeID
                };

                choices.Add(choiceData);
            }

            return choices;
        }
    }
}