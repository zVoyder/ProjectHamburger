namespace VUDK.Features.More.DialogueSystem.Events
{
    using System;
    using VUDK.Features.More.DialogueSystem.Data;

    public static class DSEvents
    {
        // Dialogue Handler Events
        public static EventHandler<OnStartDialogueEventArgs> DialogueStartHandler;
        public static Action<int> DialogueChoiceHandler;
        public static Action DialogueInterruptHandler;

        // Dialogue Generic Events
        public static Action OnStart;
        public static Action OnEnd;
        public static Action OnNext;
        public static Action OnInterrupt;
        public static Action OnCompletedSentence;
        public static Action OnEnable;
        public static Action OnDisable;
    }

    public class OnStartDialogueEventArgs : EventArgs
    {
        public DSDialogueContainerData DialogueContainerData;
        public DSDialogueData DialogueData;
        public bool RandomStart;
        public bool IsInstant;

        public OnStartDialogueEventArgs(DSDialogueContainerData dSDialogueContainerData, DSDialogueData dSDialogueData, bool randomStart, bool isInstant)
        {
            DialogueContainerData = dSDialogueContainerData;
            DialogueData = dSDialogueData;
            RandomStart = randomStart;
            IsInstant = isInstant;
        }
    }
}