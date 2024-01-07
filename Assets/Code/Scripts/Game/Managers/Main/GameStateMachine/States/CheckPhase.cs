namespace ProjectH.Managers.Main.GameStateMachine.States
{
    using System;
    using VUDK.Patterns.StateMachine;
    using VUDK.Extensions;
    using ProjectH.Managers.Main.GameStateMachine.Contexts;
    using ProjectH.Features.Grid.Tiles;
    using ProjectH.Features.Grid.Pieces;
    using ProjectH.Managers.Main.GameStateMachine.States.StateKeys;

    public class CheckPhase : State<GameContext>
    {
        public CheckPhase(Enum stateKey, StateMachine relatedStateMachine, StateContext context) : base(stateKey, relatedStateMachine, context)
        {
        }

        public override void Enter()
        {
#if UNITY_EDITOR
            DebugExtension.Advise(nameof(CheckPhase));
#endif
            if(CheckStacksOnGrid())
                ChangeState(GamePhaseKey.GamevictoryPhase);
            else
                ChangeState(GamePhaseKey.InputPhase);
        }

        public override void Exit()
        {
        }

        public override void FixedProcess()
        {
        }

        public override void Process()
        {
        }

        private bool CheckStacksOnGrid()
        {
            foreach (GameGridTile tile in Context.GameGrid.GridTiles)
            {
                if(CheckTileStack(tile))
                    return true;
            }

            return false;
        }

        private bool CheckTileStack(GameGridTile tile)
        {
            if (!tile.IsOccupied ||
                tile.StackCount != Context.LevelManager.PiecesCount) return false;

            if (tile.TopPiece.PieceType == PieceType.Closure &&
                tile.BottomPiece.PieceType == PieceType.Closure)
            {
                return true;
            }

            return false;
        }
    }
}