namespace VUDK.Features.More.DialogueSystem.Data
{
    using System.Collections.Generic;
    using UnityEngine;
    using VUDK.Features.More.DialogueSystem.Enums;

    public class DSDialogueData : ScriptableObject
    {
        public string DialogueName;
        [TextArea(3, 10)]
        public string DialogueText;
        public DSActorData ActorData;
        public AudioClip DialogueAudioClip;
        public List<DSDialogueChoiceData> Choices;
        public DSDialogueType DialogueType;
        public bool IsStartDialogue;

        public void Init(string dialogueName, DSActorData actorData, AudioClip dialogueAudioClip, string dialogueText, List<DSDialogueChoiceData> choices, DSDialogueType dialogueType, bool isStartDialogue = false)
        {
            DialogueName = dialogueName;
            DialogueText = dialogueText;
            ActorData = actorData;
            DialogueAudioClip = dialogueAudioClip;
            Choices = choices;
            DialogueType = dialogueType;
            IsStartDialogue = isStartDialogue;
        }
    }
}
