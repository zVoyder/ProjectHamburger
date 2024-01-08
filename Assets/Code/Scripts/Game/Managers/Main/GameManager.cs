namespace ProjectH.Managers.Main
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main.Bases;
    using VUDK.Patterns.Initialization.Interfaces;
    using VUDK.Features.Main.InputSystem.MobileInputs;
    using ProjectH.Features.Grid;
    using ProjectH.Managers.Main.GameStateMachine;
    using ProjectH.Features.Moves;
    using ProjectH.Features.Levels;
    using ProjectH.Features.Victory;

    public class GameManager : GameManagerBase, IInit
    {
        [field: SerializeField, Header("Level Manager")]
        public LevelManager LevelManager { get; private set; }

        [field: SerializeField, Header("Level Controller")]
        public LevelActionsController LevelController { get; private set; }

        [field: SerializeField, Header("Game Grid")]
        public GameGrid GameGrid { get; private set; }

        [field: SerializeField, Header("Animation Controller")]
        public PiecesMovesGraphicsController AnimationController { get; private set; }

        [field: SerializeField, Header("Victory Animation Controller")]
        public VictoryAnimationController VictoryAnimationController { get; private set; }

        [field: SerializeField, Header("Game Machine")]
        public GameMachine GameMachine { get; private set; }

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            if (!Check())
            {
                Debug.LogError("GameManager: Init failed. Check if all dependencies are set.");
                return;
            }

            LevelManager.Init(this);
            LevelController.Init(this);
            GameMachine.Init(GameGrid, MobileInputsManager.Ins, LevelManager);
        }

        public bool Check()
        {
            return GameGrid != null && AnimationController != null && GameMachine != null;
        }
    }
}