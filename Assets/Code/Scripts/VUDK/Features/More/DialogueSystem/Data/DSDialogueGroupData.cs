namespace VUDK.Features.More.DialogueSystem.Data
{
    using UnityEngine;

    public class DSDialogueGroupData : ScriptableObject
    {
        public string GroupName;

        public void Init(string groupName)
        {
            GroupName = groupName;
        }
    }
}