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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public override void Exit()
        {
        }

        /// <inheritdoc/>
        public override void FixedProcess()
        {
        }

        /// <inheritdoc/>
        public override void Process()
        {
        }

        /// <summary>
        /// Checks if there is a stack of pieces on the grid that is in the correct order.
        /// </summary>
        /// <returns>True if there is a stack of pieces on the grid that is in the correct order, False otherwise.</returns>
        private bool CheckStacksOnGrid()
        {
            foreach (GameGridTile tile in Context.GameGrid.GridTiles)
            {
                if(CheckTileStack(tile))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if the stack of pieces on the tile is in the correct order.
        /// </summary>
        /// <param name="tile">Tile to check.</param>
        /// <returns>True if the stack of pieces on the tile is in the correct order, False otherwise.</returns>
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