namespace VUDK.Features.More.DialogueSystem.Components
{
    using UnityEngine;
    using UnityEngine.Events;
    using VUDK.Features.More.DialogueSystem.Data;
    using VUDK.Patterns.Initialization.Interfaces;

    public class DSDialogueListener : MonoBehaviour, IInit<DSDialogueContainerData>
    {
        [Header("Dialogue Listener Settings")]
        [SerializeField]
        private DSDialogueContainerData _dialogueToListen;

        [Header("Dialogue Events")]
        public UnityEvent OnStart;
        public UnityEvent OnEnd;
        public UnityEvent OnNext;
        public UnityEvent OnInterrupt;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        public void SubscribeEvents()
        {
            if (_dialogueToListen == null) return;
            _dialogueToListen.OnStart += OnDialogueStart;
            _dialogueToListen.OnEnd += OnDialogueEnd;
            _dialogueToListen.OnNext += OnDialogueNext;
            _dialogueToListen.OnInterrupt += OnDialogueInterrupt;
        }

        public void UnsubscribeEvents()
        {
            if (_dialogueToListen == null) return;
            _dialogueToListen.OnStart -= OnDialogueStart;
            _dialogueToListen.OnEnd -= OnDialogueEnd;
            _dialogueToListen.OnNext -= OnDialogueNext;
            _dialogueToListen.OnInterrupt -= OnDialogueInterrupt;
        }

        public void Init(DSDialogueContainerData dialogueToListen)
        {
            _dialogueToListen = dialogueToListen;
            SubscribeEvents(); // Re-Subscribe
        }

        public bool Check()
        {
            return _dialogueToListen != null;
        }

        protected virtual void OnDialogueStart()
        {
            OnStart?.Invoke();
        }

        protected virtual void OnDialogueNext()
        {
            OnNext?.Invoke();
        }

        protected virtual void OnDialogueInterrupt()
        {
            OnInterrupt?.Invoke();
        }

        protected virtual void OnDialogueEnd()
        {
            OnEnd?.Invoke();
        }
    }
}