namespace VUDK.Features.More.DialogueSystem
{
    using UnityEngine;
    using VUDK.Features.More.DialogueSystem.Data;

    public abstract class DSDialogueSelectorBase : MonoBehaviour
    {
        [SerializeField]
        protected DSDialogueContainerData DialogueContainer;
        [SerializeField]
        protected DSDialogueData StartDialogue;
        [SerializeField]
        private DSDialogueGroupData _dialogueGroup;
        [SerializeField]
        protected bool RandomStartDialogue;
        [SerializeField]
        protected bool IsInstantDialogue;
        [SerializeField]
        protected bool IsRepeatable;
        [SerializeField]
        private bool _groupedDialogues;
        [SerializeField]
        private bool _startingDialoguesOnly;
        [SerializeField]
        private int _selectedDialogueGroupIndex;
        [SerializeField]
        private int _selectedDialogueIndex;

#if UNITY_EDITOR
        public static class PropertyNames
        {
            public static string DialogueContainerProperty => nameof(DialogueContainer);
            public static string DialogueProperty => nameof(StartDialogue);
            public static string RandomStartDialogueProperty => nameof(RandomStartDialogue);
            public static string IsInstantDialogueProperty => nameof(IsInstantDialogue);
            public static string IsRepeatableProperty => nameof(IsRepeatable);
            public static string DialogueGroupProperty => nameof(_dialogueGroup);
            public static string GroupedDialoguesProperty => nameof(_groupedDialogues);
            public static string StartingDialoguesOnlyProperty => nameof(_startingDialoguesOnly);
            public static string SelectedDialogueGroupIndexProperty => nameof(_selectedDialogueGroupIndex);
            public static string SelectedDialogueIndexProperty => nameof(_selectedDialogueIndex);
        }
#endif
    }
}