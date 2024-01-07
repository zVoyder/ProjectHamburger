namespace ProjectH.Features.Levels
{
    using UnityEngine;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Patterns.Initialization.Interfaces;
    using ProjectH.Constants;
    using ProjectH.Managers.Main.GameStateMachine;
    using ProjectH.Managers.Main.GameStateMachine.States.StateKeys;
    using ProjectH.Managers.Main;
    using ProjectH.Features.Moves.Undo;

    public class LevelController : MonoBehaviour, IInit<GameManager>
    {
        private GameManager _gameManager;
        private GameMachine GameMachine => _gameManager.GameMachine;

        public void Init(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        private void OnEnable()
        {
            EventManager.Ins.AddListener(EventKeys.GameEvents.OnLevelCompletedFade, LoadLevel);
        }

        private void OnDisable()
        {
            EventManager.Ins.RemoveListener(EventKeys.GameEvents.OnLevelCompletedFade, LoadLevel);
        }

        public bool Check()
        {
            return _gameManager != null;
        }

        public void TriggerNextLevel()
        {
            if (GameMachine.IsState(GamePhaseKey.InputPhase) || GameMachine.IsState(GamePhaseKey.GamevictoryPhase))
            {
                _gameManager.LevelManager.SetCurrentLevelIndexToNextLevel();
                EventManager.Ins.TriggerEvent(EventKeys.GameEvents.OnNextLevel);
            }
        }

        public void TriggerUndo()
        {
            UndoController.Undo();
        }

        public void TriggerResetLevel()
        {
            if (!GameMachine.IsState(GamePhaseKey.InputPhase)) return;

            EventManager.Ins.TriggerEvent(EventKeys.GameEvents.OnResetLevel);
            GameMachine.ChangeState(GamePhaseKey.PlacementPhase);
        }

        private void LoadLevel()
        {
            GameMachine.ChangeState(GamePhaseKey.PlacementPhase);
        }
    }
}