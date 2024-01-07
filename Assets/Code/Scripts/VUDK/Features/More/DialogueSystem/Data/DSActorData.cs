namespace VUDK.Features.More.DialogueSystem.Data
{
    using System;
    using UnityEngine;

    public class DSActorData : ScriptableObject
    {
        public Sprite ActorIcon;
        public string Name;
        public Action<AudioClip> OnEmitDialogue;

        public void Init(Sprite actorIcon, string actorName)
        {
            ActorIcon = actorIcon;
            Name = actorName;
        }
    }
}