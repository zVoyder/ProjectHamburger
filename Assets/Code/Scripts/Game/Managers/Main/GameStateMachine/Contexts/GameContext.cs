namespace ProjectH.Managers.Main.GameStateMachine.Contexts
{
    using ProjectH.Features.Grid;
    using ProjectH.Features.Levels;
    using VUDK.Features.Main.InputSystem.MobileInputs;
    using VUDK.Patterns.StateMachine;

    public class GameContext : StateContext
    {
        public GameGrid GameGrid { get; private set; }
        public MobileInputsManager MobileInputs { get; private set; }
        public LevelManager LevelManager { get; private set; }
        public UIManager UIManager { get; private set;}

        public GameContext(GameGrid gameGrid, MobileInputsManager mobileInputsManager, LevelManager levelManager, UIManager uiManager) : base()
        {
            GameGrid = gameGrid;
            MobileInputs = mobileInputsManager;
            LevelManager = levelManager;
            UIManager = uiManager;
        }
    }
}