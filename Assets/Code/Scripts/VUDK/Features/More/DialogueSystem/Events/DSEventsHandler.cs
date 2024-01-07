namespace VUDK.Features.More.DialogueSystem.Events
{
    using VUDK.Features.More.DialogueSystem.Data;

    public static class DSEventsHandler
    {
        public static void StartDialogue(object sender, DSDialogueContainerData dialogueContainerData, DSDialogueData dialogueData, bool randomStart, bool isInstant)
        {
            OnStartDialogueEventArgs args = new OnStartDialogueEventArgs(dialogueContainerData, dialogueData, randomStart, isInstant);
            DSEvents.DialogueStartHandler?.Invoke(sender, args);
        }

        public static void SendDialogueChoice(int choiceIndex)
        {
            DSEvents.DialogueChoiceHandler?.Invoke(choiceIndex);
        }

        public static void InterruptDialogue()
        {
            DSEvents.DialogueInterruptHandler?.Invoke();
        }
    }
}
