namespace ProjectH.Managers.Main
{
    using UnityEngine;
    using TMPro;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Generic.Managers.Main.Bases;
    using VUDK.Generic.Managers.Main.Interfaces;
    using VUDK.Generic.Managers.Main;
    using ProjectH.Constants;

    public class UIManager : UIManagerBase, ICastGameManager<GameManager>
    {
        [Header("Menu Panels")]
        [SerializeField]
        private GameObject _inGameMenu;
        [SerializeField]
        private GameObject _levelCompletedPanel;
        [SerializeField]
        private GameObject _bottomInGameMenu;

        [Header("Menu Texts")]
        [SerializeField]
        private TMP_Text _levelText;

        public GameManager GameManager => MainManager.Ins.GameManager as GameManager;
        public bool IsOnMenu => !_inGameMenu.activeSelf;

        private void OnEnable()
        {
            EventManager.Ins.AddListener(EventKeys.GameEvents.OnEatPhase, EnableLevelCompletedPanel);
            EventManager.Ins.AddListener(EventKeys.GameEvents.OnGameVictory, DisableBottomInGameMenu);
            EventManager.Ins.AddListener(EventKeys.GameEvents.OnGameBegin, OnGameBegin);
            //EventManager.Ins.AddListener(EventKeys.GameEvents.OnEa, DisableLevelCompletedPanel);
        }

        private void OnDisable()
        {
            EventManager.Ins.RemoveListener(EventKeys.GameEvents.OnEatPhase, EnableLevelCompletedPanel);
            EventManager.Ins.AddListener(EventKeys.GameEvents.OnGameVictory, DisableBottomInGameMenu);
            EventManager.Ins.RemoveListener(EventKeys.GameEvents.OnGameBegin, OnGameBegin);
        }

        private void EnableLevelCompletedPanel()
        {
            _levelCompletedPanel.gameObject.SetActive(true);
        }

        private void DisableLevelCompletedPanel()
        {
            _levelCompletedPanel.gameObject.SetActive(false);
        }

        private void EnableBottomInGameMenu()
        {
            _bottomInGameMenu.gameObject.SetActive(true);
        }

        private void DisableBottomInGameMenu()
        {
            _bottomInGameMenu.gameObject.SetActive(false);
        }

        private void OnGameBegin()
        {
            EnableBottomInGameMenu();
            DisableLevelCompletedPanel();
            _levelText.text = $"Level {GameManager.LevelManager.CurrentLevelIndex + 1}";
        }
    }
}