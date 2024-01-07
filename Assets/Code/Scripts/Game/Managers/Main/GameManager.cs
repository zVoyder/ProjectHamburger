namespace ProjectH.Managers.Main
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main.Bases;
    using VUDK.Patterns.Initialization.Interfaces;
    using VUDK.Features.Main.InputSystem.MobileInputs;
    using ProjectH.Features.Grid;
    using ProjectH.Managers.Main.GameStateMachine;
    using ProjectH.Features.Levels.Data;
    using ProjectH.Features.Levels;
    using ProjectH.Features.Moves;

    public class GameManager : GameManagerBase, IInit
    {
        [field: SerializeField, Header("Game Grid")]
        public GameGrid GameGrid { get; private set; }

        [field: SerializeField, Header("Animation Controller")]
        public AnimationController AnimationController { get; private set; }

        [field: SerializeField, Header("Game Machine")]
        public GameMachine GameMachine { get; private set; }

#if UNITY_EDITOR
        public LevelData LevelToSelect;
#endif

        private void Start()
        {
#if UNITY_EDITOR
            LevelManager.SelectLevel(LevelToSelect);
#endif
            Init(); // TODO: Change this on input start game
        }

        public void Init()
        {
            if(!Check())
            {
                Debug.LogError("GameManager: Init failed. Check if all dependencies are set.");
                return;
            }

            GameGrid.Init(LevelManager.SelectedLevel);
            GameMachine.Init(GameGrid, MobileInputsManager.Ins);
        }

        public bool Check()
        {
            return GameGrid != null && AnimationController != null && GameMachine != null;
        }
    }
}