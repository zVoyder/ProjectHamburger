namespace ProjectH.Managers.Main.GameStateMachine
{
    using ProjectH.Features.Grid;
    using ProjectH.Managers.Main.GameStateMachine.Contexts;
    using ProjectH.Managers.Main.GameStateMachine.States;
    using ProjectH.Managers.Main.GameStateMachine.States.StateKeys;
    using ProjectH.Patterns.Factories;
    using ProjectH.Features.Levels;
    using VUDK.Generic.Managers.Main.Bases;
    using VUDK.Patterns.Initialization.Interfaces;
    using VUDK.Features.Main.InputSystem.MobileInputs;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces;

    public class GameMachine : GameMachineBase, IInit<GameGrid, MobileInputsManager, LevelManager>, ICastUIManager<UIManager>
    {
        private GameContext _context;

        public UIManager UIManager => MainManager.Ins.UIManager as UIManager;

        /// <inheritdoc/>
        public void Init(GameGrid grid, MobileInputsManager mobileInputs, LevelManager levelManager)
        {
            _context = MachineFactory.CreateGameContext(grid, mobileInputs, levelManager, UIManager);
            Init();
        }

        /// <inheritdoc/>
        public override void Init()
        {
            base.Init();

            PlacementPhase placementPhase = MachineFactory.CreateGamePhase<PlacementPhase>(GamePhaseKey.PlacementPhase, this, _context);
            InputPhase inputPhase = MachineFactory.CreateGamePhase<InputPhase>(GamePhaseKey.InputPhase, this, _context);
            MovePhase movePhase = MachineFactory.CreateGamePhase<MovePhase>(GamePhaseKey.MovePhase, this, _context);
            CheckPhase checkPhase = MachineFactory.CreateGamePhase<CheckPhase>(GamePhaseKey.CheckPhase, this, _context);
            GamevictoryPhase gamevictoryPhase = MachineFactory.CreateGamePhase<GamevictoryPhase>(GamePhaseKey.GamevictoryPhase, this, _context);

            AddState(GamePhaseKey.PlacementPhase, placementPhase);
            AddState(GamePhaseKey.InputPhase, inputPhase);
            AddState(GamePhaseKey.MovePhase, movePhase);
            AddState(GamePhaseKey.CheckPhase, checkPhase);
            AddState(GamePhaseKey.GamevictoryPhase, gamevictoryPhase);

            ChangeState(GamePhaseKey.PlacementPhase);
        }

        /// <inheritdoc/>
        public override bool Check()
        {
            return _context != null;
        }
    }
}
