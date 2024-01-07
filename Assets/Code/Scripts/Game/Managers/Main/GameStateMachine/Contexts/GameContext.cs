namespace ProjectH.Managers.Main.GameStateMachine.Contexts
{
    using ProjectH.Features.Grid;
    using ProjectH.Managers.Main;
    using VUDK.Features.Main.InputSystem.MobileInputs;
    using VUDK.Patterns.StateMachine;

    public class GameContext : StateContext
    {
        public GameGrid GameGrid { get; private set; }
        public MobileInputsManager MobileInputs { get; private set; }

        public GameContext(GameGrid gameGrid, MobileInputsManager mobileInputsManager) : base()
        {
            GameGrid = gameGrid;
            MobileInputs = mobileInputsManager;
        }
    }
}