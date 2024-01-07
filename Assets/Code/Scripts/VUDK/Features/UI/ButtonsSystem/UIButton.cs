namespace VUDK.Features.UI.ButtonsSystem
{
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;
    using VUDK.Constants;
    using VUDK.Features.Main.EventSystem;

    [RequireComponent(typeof(Button))]
    public class UIButton : MonoBehaviour
    {
        protected Button Button { get; private set; }

        [Header("Events")]
        public UnityEvent OnButtonPressedSuccess;
        public UnityEvent OnButtonPressedFail;

        protected virtual void Awake()
        {
            TryGetComponent(out Button button);
            Button = button;
        }

        protected virtual void OnEnable()
        {
            Button.onClick.AddListener(Press);
        }

        protected virtual void OnDisable()
        {
            Button.onClick.RemoveListener(Press);
        }

        /// <summary>
        /// Triggers the press of this button.
        /// </summary>
        protected virtual void Press()
        {
            EventManager.Ins.TriggerEvent(EventKeys.UIEvents.OnButtonPressed);
            OnButtonPressedSuccess?.Invoke();
        }

        /// <summary>
        /// Disables the interaction of this button.
        /// </summary>
        protected virtual void Enable()
        {
            Button.interactable = true;
        }

        /// <summary>
        /// Enables the interaction of this button.
        /// </summary>
        protected virtual void Disable()
        {
            Button.interactable = false;
        }
    }
}
