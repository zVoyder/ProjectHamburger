namespace VUDK.Features.More.DialogueSystem.Editor.Data.Save
{
    using System.Collections.Generic;
    using UnityEngine;
    using VUDK.Features.More.DialogueSystem.Data;
    using VUDK.Features.More.DialogueSystem.Enums;

    [System.Serializable]
    public class DSNodeEditorData
    {
        public string NodeID;
        public string GroupID;
        public Vector2 Position;
        public string Name;
        public string DialogueText;
        public DSActorData ActorData;
        public AudioClip DialogueAudioClip;
        public DSDialogueType DialogueType;
        public List<DSChoiceEditorData> Choices;
    }
}