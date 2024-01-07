namespace VUDK.Features.More.DialogueSystem.Components
{
    using System.Collections.Generic;
    using UnityEngine;
    using VUDK.Features.More.DialogueSystem.Components.Interfaces;
    using VUDK.Features.More.DialogueSystem.Data;
    using VUDK.Features.More.DialogueSystem.Events;

    [RequireComponent(typeof(DSDialogueListener))]
    public class DSDialogueTriggerListener : MonoBehaviour, IDialogueTrigger
    {
        [Header("Dialogue Trigger Listener Settings")]
        [SerializeField]
        private bool _isInstant;
        [SerializeField]
        private List<DSDialogueContainerData> _possibleDialogues;

        private bool _isSpeaking;
        private DSDialogueContainerData _pickedDialogue;
        private DSDialogueListener _dialogueListener;

        private void Awake()
        {
            TryGetComponent(out _dialogueListener);
        }

        public void Trigger()
        {
            if (_possibleDialogues == null || _possibleDialogues.Count == 0 || _isSpeaking)
                return;

            _pickedDialogue = _possibleDialogues[Random.Range(0, _possibleDialogues.Count)];

            DSEvents.DialogueStartHandler?.Invoke(
                this,
                new OnStartDialogueEventArgs(_pickedDialogue, null, true, _isInstant)
                );

            _isSpeaking = true;
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _pickedDialogue.OnEnd += OnDialogueEnded;
            _dialogueListener.Init(_pickedDialogue);
        }

        private void UnsubscribeEvents()
        {
            _pickedDialogue.OnEnd -= OnDialogueEnded;
            _dialogueListener.UnsubscribeEvents();
        }

        public void Interrupt()
        {
            DSEventsHandler.InterruptDialogue();
            _isSpeaking = false;
        }

        private void OnDialogueEnded()
        {
            _isSpeaking = false;
            UnsubscribeEvents();
        }
    }
}