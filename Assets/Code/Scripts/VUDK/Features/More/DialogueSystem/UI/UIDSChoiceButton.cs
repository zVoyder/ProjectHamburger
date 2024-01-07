namespace VUDK.Features.More.DialogueSystem.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using VUDK.Features.More.DialogueSystem.Events;
    using VUDK.Patterns.Initialization.Interfaces;

    [RequireComponent(typeof(Button))]
    public class UIDSChoiceButton : MonoBehaviour, IInit<int>
    {
        [field: SerializeField]
        public Button Button { get; private set; }

        [field: SerializeField]
        public TMP_Text Text { get; private set; }

        private int _choiceIndex;

        private void OnValidate()
        {
            TryGetComponent(out Button button);
            Text = GetComponentInChildren<TMP_Text>();
            Button = button;
        }

        private void OnEnable()
        {
            Button.onClick.AddListener(SelectChoice);
        }

        private void OnDisable()
        {
            Button.onClick.RemoveListener(SelectChoice);
        }

        public void Init(int choiceIndex)
        {
            _choiceIndex = choiceIndex;
        }

        public bool Check()
        {
            return Button != null && Text != null;
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        private void SelectChoice()
        {
            DSEventsHandler.SendDialogueChoice(_choiceIndex);
        }
    }
}