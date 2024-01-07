namespace VUDK.Features.More.DialogueSystem.Data
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using VUDK.Generic.Serializable;

    public class DSDialogueContainerData : ScriptableObject
    {
        public string FileName;
        public SerializableDictionary<DSDialogueGroupData, List<DSDialogueData>> DialogueGroups;
        public List<DSDialogueData> UngroupedDialogues;
        public List<DSDialogueData> StartingDialogues;
        
        public Action OnStart;
        public Action OnEnd;
        public Action OnNext;
        public Action OnInterrupt;

        public void Init(string fileName)
        {
            FileName = fileName;

            DialogueGroups = new SerializableDictionary<DSDialogueGroupData, List<DSDialogueData>>();
            UngroupedDialogues = new List<DSDialogueData>();
            StartingDialogues = new List<DSDialogueData>();
        }

        public List<string> GetDialogueGroupNames()
        {
            List<string> dialogueGroupNames = new List<string>();

            foreach (DSDialogueGroupData dialogueGroup in DialogueGroups.Keys)
            {
                dialogueGroupNames.Add(dialogueGroup.GroupName);
            }

            return dialogueGroupNames;
        }

        public List<string> GetGroupedDialogueNames(DSDialogueGroupData dialogueGroup, bool startingDialoguesOnly)
        {
            List<DSDialogueData> groupedDialogues = DialogueGroups[dialogueGroup];

            List<string> groupedDialogueNames = new List<string>();

            foreach (DSDialogueData groupedDialogue in groupedDialogues)
            {
                if (startingDialoguesOnly && !groupedDialogue.IsStartDialogue)
                    continue;

                groupedDialogueNames.Add(groupedDialogue.DialogueName);
            }

            return groupedDialogueNames;
        }

        public List<string> GetUngroupedDialogueNames(bool startingDialoguesOnly)
        {
            List<string> ungroupedDialogueNames = new List<string>();

            foreach (DSDialogueData ungroupedDialogue in UngroupedDialogues)
            {
                if (startingDialoguesOnly && !ungroupedDialogue.IsStartDialogue)
                    continue;

                ungroupedDialogueNames.Add(ungroupedDialogue.DialogueName);
            }

            return ungroupedDialogueNames;
        }
    }
}