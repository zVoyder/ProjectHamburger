namespace VUDK.Features.More.DialogueSystem.Editor.Windows
{
    using System;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEditor.Experimental.GraphView;
    using UnityEngine;
    using UnityEngine.UIElements;
    using VUDK.Extensions;
    using VUDK.Features.More.DialogueSystem.Editor.Data.Error;
    using VUDK.Features.More.DialogueSystem.Editor.Data.Save;
    using VUDK.Features.More.DialogueSystem.Editor.Elements;
    using VUDK.Features.More.DialogueSystem.Editor.Utilities;
    using VUDK.Features.More.DialogueSystem.Enums;
    using VUDK.Generic.Serializable;

    public class DSGraphView : GraphView
    {
        private DSEditorWindow _editorWindow;
        private DSSearchWindow _searchWindow;
        private MiniMap _miniMap;

        private SerializableDictionary<string, DSNodeErrorData> _ungroupedNodes;
        private SerializableDictionary<string, DSGroupErrorData> _groups;
        private SerializableDictionary<Group, Dictionary<string, DSNodeErrorData>> _groupedNodes;
        private int _nameErrorsAmount;

        public int NameErrorsAmount 
        {
            get
            {
                return _nameErrorsAmount;
            }
            set
            {
                _nameErrorsAmount = value;
                
                if(_nameErrorsAmount == 0)
                {
                    _editorWindow.EnableSaving();
                }

                if(_nameErrorsAmount == 1)
                {
                    _editorWindow.DisableSaving();
                }
            }
        }

        public DSGraphView(DSEditorWindow editorWindow)
        {
            _editorWindow = editorWindow;
            _groups = new SerializableDictionary<string, DSGroupErrorData>();
            _ungroupedNodes = new SerializableDictionary<string, DSNodeErrorData>();
            _groupedNodes = new SerializableDictionary<Group, Dictionary<string, DSNodeErrorData>>();

            AddManipulators();
            AddSearchWindow();
            AddMiniMap();
            AddGridBackground();

            OnElementsDeleted();
            OnGroupElementsAdded();
            OnGroupElementsRemoved();
            OnGroupRenamed();
            OnGraphViewChanged();

            AddStyles();
            AddMiniMapStyles();
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            List<Port> compatiblePorts = new List<Port>();
            ports.ForEach(port =>
            {
                if (startPort == port) return;
                if (startPort.node == port.node) return;
                if (startPort.direction == port.direction) return;

                compatiblePorts.Add(port);
            });
            return compatiblePorts;
        }

        public DSGroup CreateGroup(string title, Vector2 localMousePosition)
        {
            DSGroup group = new DSGroup(title, localMousePosition);
            AddGroup(group);
            AddElement(group);

            foreach(GraphElement selectedElement in selection)
            {
                if (selectedElement is not DSNode)
                    continue;

                DSNode node = selectedElement as DSNode;
                group.AddElement(node);
            }

            return group;
        }

        public void ClearGraph()
        {
            graphElements.ForEach(element =>
            {
                RemoveElement(element);
            });

            _groups.Clear();
            _groupedNodes.Clear();
            _ungroupedNodes.Clear();
            NameErrorsAmount = 0;
        }

        public void ToggleMiniMap()
        {
            _miniMap.visible = !_miniMap.visible;
        }

        public Vector2 GetLocalMousePosition(Vector2 mousePosition, bool isSearchWindow = false)
        {
            Vector2 worldMousePosition = mousePosition;

            if (isSearchWindow)
                worldMousePosition -= _editorWindow.position.position;

            Vector2 localMousePosition = contentViewContainer.WorldToLocal(worldMousePosition);
            return localMousePosition;
        }

        public DSNode CreateNode(string nodeName, DSDialogueType dialogueType, Vector2 position, bool shouldDraw = true)
        {
            Type nodeType = Type.GetType($"VUDK.Features.More.DialogueSystem.Editor.Elements.DS{dialogueType}Node");
            DSNode node = Activator.CreateInstance(nodeType) as DSNode;
            node.Init(nodeName, position, this);

            if (shouldDraw)
            {
                node.Draw();
            }

            AddUngroupedNode(node);
            return node;
        }

        public void AddUngroupedNode(DSNode node)
        {
            string nodeName = node.DialogueName.ToLower();

            if (!_ungroupedNodes.ContainsKey(nodeName))
            {
                DSNodeErrorData nodeErrorData = new DSNodeErrorData();
                nodeErrorData.Nodes.Add(node);
                _ungroupedNodes.Add(nodeName, nodeErrorData);
                return;
            }

            List<DSNode> ungroupedList = _ungroupedNodes[nodeName].Nodes;
            ungroupedList.Add(node);

            Color errorColor = _ungroupedNodes[nodeName].ErrorData.Color;
            node.SetErrorStyle(errorColor);

            if (ungroupedList.Count == 2)
            {
                NameErrorsAmount++;
                ungroupedList[0].SetErrorStyle(errorColor);
            }
        }

        public void RemoveUngroupedNode(DSNode node)
        {
            string nodeName = node.DialogueName.ToLower();
            List<DSNode> ungroupedNodesList = _ungroupedNodes[nodeName].Nodes;

            ungroupedNodesList.Remove(node);
            node.ResetStyle();

            if (ungroupedNodesList.Count == 1)
            {
                NameErrorsAmount--;
                ungroupedNodesList[0].ResetStyle();
                return;
            }

            if (ungroupedNodesList.Count == 0)
                _ungroupedNodes.Remove(nodeName);
        }

        public void AddNodeInGroup(DSNode node, DSGroup group)
        {
            string nodeName = node.DialogueName.ToLower();
            node.Group = group;

            if (!_groupedNodes.ContainsKey(group)) // Check if the group is in the dictionary
            {
                _groupedNodes.Add(group, new Dictionary<string, DSNodeErrorData>());
            }

            if (!_groupedNodes[group].ContainsKey(nodeName)) // Check if the node is in the group
            {
                DSNodeErrorData nodeErrorData = new DSNodeErrorData();
                nodeErrorData.Nodes.Add(node);
                _groupedNodes[group].Add(nodeName, nodeErrorData);
                return;
            }

            List<DSNode> groupedList = _groupedNodes[group][nodeName].Nodes;
            groupedList.Add(node);

            Color errorColor = _groupedNodes[group][nodeName].ErrorData.Color;
            node.SetErrorStyle(errorColor);

            if (groupedList.Count == 2)
            {
                NameErrorsAmount++;
                groupedList[0].SetErrorStyle(errorColor);
            }
        }

        public void RemoveNodeFromGroup(DSNode node, Group group)
        {
            string nodeName = node.DialogueName.ToLower();
            node.Group = null;

            List<DSNode> groupedList = _groupedNodes[group][nodeName].Nodes;
            groupedList.Remove(node);
            node.ResetStyle();

            if (groupedList.Count == 1)
            {
                NameErrorsAmount--;
                groupedList[0].ResetStyle();
                return;
            }

            if (groupedList.Count == 0)
            {
                _groupedNodes[group].Remove(nodeName);

                if (_groupedNodes[group].Count == 0)
                    _groupedNodes.Remove(group);
            }
        }

        private void AddGroup(DSGroup group)
        {
            string groupName = group.title.ToLower();

            if (!_groups.ContainsKey(groupName)) // Check if there is a group with the same name
            {
                DSGroupErrorData groupErrorData = new DSGroupErrorData();
                groupErrorData.Groups.Add(group);
                _groups.Add(groupName, groupErrorData);
                return;
            }

            // If there is a group with the same name, set the error style to the groups
            List<DSGroup> groupList = _groups[groupName].Groups;
            groupList.Add(group);

            Color errorColor = _groups[groupName].ErrorData.Color;
            group.SetErrorStyle(errorColor);

            if (groupList.Count == 2)
            {
                NameErrorsAmount++;
                groupList[0].SetErrorStyle(errorColor);
            }
        }

        private void RemoveGroup(DSGroup group)
        {
            string oldGroupName = group.OldTitle.ToLower();

            List<DSGroup> groupList = _groups[oldGroupName].Groups;
            groupList.Remove(group);
            group.ResetStyle();

            if (groupList.Count == 1)
            {
                NameErrorsAmount--;
                groupList[0].ResetStyle();
                return;
            }

            if (groupList.Count == 0)
                _groups.Remove(oldGroupName);
        }

        private void AddSearchWindow()
        {
            if (!_searchWindow)
            {
                _searchWindow = ScriptableObject.CreateInstance<DSSearchWindow>();
                _searchWindow.Init(this);
            }

            nodeCreationRequest += context => SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), _searchWindow);
        }

        private void AddManipulators()
        {
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(CreateNodeContextualMenu(DSDialogueType.SingleChoice, "Add Node (Single Choice)"));
            this.AddManipulator(CreateNodeContextualMenu(DSDialogueType.MultipleChoice, "Add Node (Multiple Choice)"));
            this.AddManipulator(CreateGroupContextualMenu());
        }

        private IManipulator CreateGroupContextualMenu()
        {
            ContextualMenuManipulator contextualMenuManipulator = new ContextualMenuManipulator(
                menuEvent => menuEvent.menu.AppendAction("Add Group", actionEvent => CreateGroup("DialogueGroup", GetLocalMousePosition(actionEvent.eventInfo.localMousePosition)))
            );

            return contextualMenuManipulator;
        }

        private IManipulator CreateNodeContextualMenu(DSDialogueType dialogueType, string actionTitle)
        {
            ContextualMenuManipulator contextualMenuManipulator = new ContextualMenuManipulator(
                menuEvent => menuEvent.menu.AppendAction(actionTitle,
                    actionEvent => AddElement(CreateNode("DialogueName", dialogueType, GetLocalMousePosition(actionEvent.eventInfo.localMousePosition))))
                );
            return contextualMenuManipulator;
        }

        private void AddStyles()
        {
            this.AddStyleSheets(
                "DSGraphViewStyles.uss",
                "DSNodeStyles.uss"
            );
        }

        private void AddMiniMapStyles()
        {
            StyleColor backgroundColor = new StyleColor(new Color32(29, 29, 30, 255));
            StyleColor borderColor = new StyleColor(new Color32(51, 51, 51, 255));

            _miniMap.style.backgroundColor = backgroundColor;
            _miniMap.style.borderTopColor = borderColor;
            _miniMap.style.borderBottomColor = borderColor;
            _miniMap.style.borderLeftColor = borderColor;
            _miniMap.style.borderRightColor = borderColor;
        }

        private void AddGridBackground()
        {
            GridBackground gridBackground = new GridBackground();
            gridBackground.StretchToParentSize();
            Insert(0, gridBackground);
        }

        private void AddMiniMap()
        {
            _miniMap = new MiniMap()
            {
            };

            _miniMap.SetPosition(new Rect(15, 50, 200, 180));
            Add(_miniMap);
            _miniMap.visible = false;
        }

        private void OnElementsDeleted()
        {
            deleteSelection = (operationName, askUser) =>
            {
                Type groupType = typeof(DSGroup);
                Type edgeType = typeof(Edge);

                List<DSGroup> groupsToDelete = new List<DSGroup>();
                List<Edge> edgesToDelete = new List<Edge>();
                List<DSNode> nodesToDelete = new List<DSNode>();

                foreach (GraphElement element in selection)
                {
                    if (element is DSNode node)
                    {
                        nodesToDelete.Add(node);
                        continue;
                    }

                    if (element.GetType() == edgeType)
                    {
                        Edge edge = element as Edge;
                        edgesToDelete.Add(edge);
                        continue;
                    }

                    if (element.GetType() != groupType)
                        continue;

                    DSGroup group = element as DSGroup;
                    RemoveGroup(group);
                    groupsToDelete.Add(group);
                }

                foreach (DSGroup group in groupsToDelete)
                {
                    List<DSNode> groupNodes = new List<DSNode>();

                    foreach(GraphElement groupElement in group.containedElements)
                    {
                        if (groupElement is not DSNode node)
                            continue;

                        DSNode groupNode = groupElement as DSNode;
                        groupNodes.Add(groupNode);
                    }

                    group.RemoveElements(groupNodes);
                    RemoveElement(group);
                }

                DeleteElements(edgesToDelete);

                foreach (DSNode node in nodesToDelete)
                {
                    if (node.Group != null)
                    {
                        node.Group.RemoveElement(node);
                    }

                    RemoveUngroupedNode(node);
                    node.DisconnectAllPorts();
                    RemoveElement(node);
                }
            };
        }

        private void OnGroupElementsAdded()
        {
            elementsAddedToGroup = (group, elements) =>
            {
                foreach (GraphElement element in elements)
                {
                    if (element is not DSNode)
                        continue;

                    DSGroup nodeGroup = group as DSGroup;
                    DSNode node = element as DSNode;
                    RemoveUngroupedNode(node);
                    AddNodeInGroup(node, nodeGroup);
                }
            };
        }

        private void OnGroupElementsRemoved()
        {
            elementsRemovedFromGroup = (group, elements) =>
            {
                foreach (GraphElement element in elements)
                {
                    if (element is not DSNode)
                        continue;

                    DSNode node = element as DSNode;
                    RemoveNodeFromGroup(node, group);
                    AddUngroupedNode(node);
                }
            };
        }

        private void OnGroupRenamed()
        {
            groupTitleChanged = (group, newTitle) =>
            {
                DSGroup dsGroup = (DSGroup)group;
                dsGroup.title = newTitle.RemoveSpecialAndWhitespaces();

                if (string.IsNullOrEmpty(dsGroup.title))
                {
                    if (!string.IsNullOrEmpty(dsGroup.OldTitle))
                    {
                        ++NameErrorsAmount;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(dsGroup.OldTitle))
                    {
                        --NameErrorsAmount;
                    }
                }

                RemoveGroup(dsGroup);
                dsGroup.OldTitle = dsGroup.title;
                AddGroup(dsGroup);
            };
        }

        private void OnGraphViewChanged()
        {
            graphViewChanged = (changes) =>
            {
                if (changes.edgesToCreate != null)
                {
                    foreach(Edge edge in changes.edgesToCreate)
                    {
                        DSNode nextNode = edge.input.node as DSNode;
                        DSChoiceEditorData choiceData = edge.output.userData as DSChoiceEditorData;
                        choiceData.NodeID = nextNode.NodeID;
                    }
                }

                if (changes.elementsToRemove != null)
                {
                    Type edgeType = typeof(Edge);

                    foreach(GraphElement element in changes.elementsToRemove)
                    {
                        if (element.GetType() != edgeType)
                            continue;

                        Edge edge = element as Edge;
                        DSChoiceEditorData choiceData = edge.output.userData as DSChoiceEditorData;
                        choiceData.NodeID = string.Empty;
                    }
                }

                return changes;
            };
        }
    }
}