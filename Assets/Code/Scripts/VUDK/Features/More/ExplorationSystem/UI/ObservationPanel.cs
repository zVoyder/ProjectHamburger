namespace ProjectH.Features.UI.UIExploration
{
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Features.More.ExplorationSystem.Constants;
    using VUDK.Features.Main.EventSystem;

    public class ObservationPanel : MonoBehaviour
    {
        [Header("Panel Settings")]
        [SerializeField]
        private RectTransform _panel;
        [SerializeField]
        private Button _exitViewButton;

        private void Awake()
        {
            Disable();
        }

        private void OnEnable()
        {
            EventManager.Ins.AddListener(ExplorationEventKeys.OnEnterNodeView, EnterObservation);
            EventManager.Ins.AddListener(ExplorationEventKeys.OnExitNodeView, ExitObservation);
            _exitViewButton.onClick.AddListener(OnExitObservationButtonHanlder);
        }

        private void OnDisable()
        {
            EventManager.Ins.RemoveListener(ExplorationEventKeys.OnEnterNodeView, EnterObservation);
            EventManager.Ins.RemoveListener(ExplorationEventKeys.OnExitNodeView, ExitObservation);
            _exitViewButton.onClick.RemoveListener(OnExitObservationButtonHanlder);
        }

        /// <summary>
        /// Enables the panel.
        /// </summary>
        public void Enable()
        {
            _panel.gameObject.SetActive(true);
        }
        
        /// <summary>
        /// Disables the panel.
        /// </summary>
        public void Disable()
        {
            _panel.gameObject.SetActive(false);
        }

        /// <summary>
        /// Called when the player enters the observation mode.
        /// </summary>
        private void EnterObservation()
        {
            Enable();
        }

        /// <summary>
        /// Called when the player exits the observation mode.
        /// </summary>
        private void ExitObservation()
        {
            Disable();
        }

        /// <summary>
        /// Called when the player clicks on the exit observation button.
        /// </summary>
        private void OnExitObservationButtonHanlder()
        {
            EventManager.Ins.TriggerEvent(ExplorationEventKeys.OnExitObservationButton);
        }
    }
}