namespace ProjectH.Patterns.Factories
{
    using ProjectH.Features.Grid;
    using ProjectH.Features.Levels;
    using ProjectH.Managers.Main;
    using ProjectH.Managers.Main.GameStateMachine.Contexts;
    using ProjectH.Managers.Main.GameStateMachine.States;
    using ProjectH.Managers.Main.GameStateMachine.States.StateKeys;
    using VUDK.Features.Main.InputSystem.MobileInputs;
    using VUDK.Patterns.StateMachine;

    /// <summary>
    /// Factory responsible for creating game-related state-machine states and contexts.
    /// </summary>
    public static class MachineFactory
    {
        /// <summary>
        /// Creates a new game context.
        /// </summary>
        /// <param name="gameGrid">The game grid.</param>
        /// <param name="mobileInputsManager">The mobile inputs manager.</param>
        /// <param name="levelManager">The level manager.</param>
        /// <param name="uiManager">The UI manager.</param>
        /// <returns>A new game context.</returns>
        public static GameContext CreateGameContext(GameGrid gameGrid, MobileInputsManager mobileInputsManager, LevelManager levelManager, UIManager uiManager)
        {
            return new GameContext(gameGrid, mobileInputsManager, levelManager, uiManager);
        }

        public static T CreateGamePhase<T>(GamePhaseKey phaseKey, StateMachine relatedStateMachine, StateContext context) where T : State<GameContext>
        {
            switch (phaseKey)
            {
                case GamePhaseKey.PlacementPhase:
                    return new PlacementPhase(GamePhaseKey.PlacementPhase, relatedStateMachine, context) as T;
                case GamePhaseKey.InputPhase:
                    return new InputPhase(GamePhaseKey.InputPhase, relatedStateMachine, context) as T;
                case GamePhaseKey.MovePhase:
                    return new MovePhase(GamePhaseKey.MovePhase, relatedStateMachine, context) as T;
                case GamePhaseKey.CheckPhase:
                    return new CheckPhase(GamePhaseKey.CheckPhase, relatedStateMachine, context) as T;
                case GamePhaseKey.GamevictoryPhase:
                    return new GamevictoryPhase(GamePhaseKey.GamevictoryPhase, relatedStateMachine, context) as T;
                default:
                    return null;
            }
        }
    }
}