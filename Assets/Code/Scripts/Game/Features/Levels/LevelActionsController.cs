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

    public class LevelActionsController : MonoBehaviour, IInit<GameManager>
    {
        private GameManager _gameManager;
        private GameMachine GameMachine => _gameManager.GameMachine;

        private bool IsLoadingLevel => _gameManager.LevelManager.IsLoadingLevel;

        private void OnEnable()
        {
            EventManager.Ins.AddListener(EventKeys.GameEvents.OnEatPhaseFadeInEnd, LoadLevel);
        }

        private void OnDisable()
        {
            EventManager.Ins.RemoveListener(EventKeys.GameEvents.OnEatPhaseFadeInEnd, LoadLevel);
        }

        /// <inheritdoc/>
        public void Init(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        /// <inheritdoc/>
        public bool Check()
        {
            return _gameManager != null;
        }

        /// <summary>
        /// Triggers the loading of the next level.
        /// </summary>
        public void TriggerNextLevel()
        {
            if (!IsLoadingLevel && GameMachine.IsState(GamePhaseKey.GamevictoryPhase))
            {
                _gameManager.LevelManager.SetCurrentLevelIndexToNextLevel();
                EventManager.Ins.TriggerEvent(EventKeys.GameEvents.OnNextLevelTriggered);
                EventManager.Ins.TriggerEvent(EventKeys.GameEvents.OnEatTapped);
            }
        }

        /// <summary>
        /// Skip the current level and triggers the loading of the next level.
        /// </summary>
        public void TriggerSkipLevel()
        {
            if (!IsLoadingLevel && GameMachine.IsState(GamePhaseKey.InputPhase))
            {
                _gameManager.LevelManager.SetCurrentLevelIndexToNextLevel();
                EventManager.Ins.TriggerEvent(EventKeys.GameEvents.OnNextLevelTriggered);
            }
        }

        /// <summary>
        /// Triggers the undo of the last move.
        /// </summary>
        public void TriggerUndo()
        {
            UndoController.Undo();
        }

        /// <summary>
        /// Triggers the reset of the current level.
        /// </summary>
        public void TriggerResetLevel()
        {
            if (!GameMachine.IsState(GamePhaseKey.InputPhase)) return;

            EventManager.Ins.TriggerEvent(EventKeys.GameEvents.OnResetLevel);
            GameMachine.ChangeState(GamePhaseKey.PlacementPhase);
        }

        /// <summary>
        /// Loads the current level.
        /// </summary>
        private void LoadLevel()
        {
            GameMachine.ChangeState(GamePhaseKey.PlacementPhase);
        }
    }
}