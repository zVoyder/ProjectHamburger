namespace VUDK.Features.More.DialogueSystem.Components
{
    using VUDK.Features.More.DialogueSystem.Components.Interfaces;
    using VUDK.Features.More.DialogueSystem.Events;

    public class DSDialogueTrigger : DSDialogueSelectorBase, IDialogueTrigger
    {
        private bool _hasBeenTriggered;
        private bool _isSpeaking;

        private void OnEnable()
        {
            DialogueContainer.OnEnd += OnDialogueEnded;
        }

        private void OnDisable()
        {
            DialogueContainer.OnEnd -= OnDialogueEnded;
        }

        public virtual void Trigger()
        {
            if( (!IsRepeatable && _hasBeenTriggered) || _isSpeaking)
                return;

            _isSpeaking = true;
            _hasBeenTriggered = true;

            if (DialogueContainer == null || DialogueContainer.StartingDialogues.Count == 0)
                return;

            DSEvents.DialogueStartHandler?.Invoke(
                this, 
                new OnStartDialogueEventArgs(DialogueContainer, StartDialogue, RandomStartDialogue, IsInstantDialogue)
                );
        }

        public void Interrupt()
        {
            DSEventsHandler.InterruptDialogue();
            _isSpeaking = false;
        }

        private void OnDialogueEnded()
        {
            _isSpeaking = false;
        }
    }
}
