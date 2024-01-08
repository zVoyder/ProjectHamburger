namespace ProjectH.Features.Moves
{
    using System.Collections.Generic;
    using VUDK.Extensions;
    using VUDK.Generic.Managers.Main;
    using ProjectH.Features.Grid.Tiles;
    using ProjectH.Features.Grid.Pieces;
    using ProjectH.Managers.Main;

    public static class InputMoveController
    {
        private static GameManager s_gameManager => MainManager.Ins.GameManager as GameManager;
        private static GameGridTile[,] s_gridTiles => s_gameManager.GameGrid.GridTiles;
        private static PiecesMovesGraphicsController s_animController => s_gameManager.AnimationController;

        /// <summary>
        /// Insert a list of piece in a new tile stack.
        /// </summary>
        /// <param name="oldStack">Old stack to move.</param>
        /// <param name="piece">Piece to move.</param>
        /// <param name="oldTile">Old tile.</param>
        /// <param name="newTile">New tile.</param>
        public static void InsertStackInTile(List<Piece> oldStack, Piece piece, GameGridTile oldTile, GameGridTile newTile)
        {
            List<Piece> reversedStack = new List<Piece>(oldStack);
            reversedStack.Reverse();

            foreach (Piece p in reversedStack)
                p.PlaceInTile(newTile);

            piece.StackOnTile(newTile);
            newTile.AddToStack(reversedStack);
            oldTile.RemoveFromStack(reversedStack); // Do not use ClearStack() because it's not correct for Undo
        }

        /// <summary>
        /// Checks if piece can move in specified direction.
        /// </summary>
        /// <param name="piece">Piece to move.</param>
        /// <param name="direction">Direction to move.</param>
        /// <returns>True if piece can move, False otherwise.</returns>
        public static bool TryMovePiece(Piece piece, Vector2Direction direction)
        {
            if (piece == null) return false;

            switch (direction)
            {
                case Vector2Direction.Up:
                    return TryMoveUp(piece);
                case Vector2Direction.Down:
                    return TryMoveDown(piece);
                case Vector2Direction.Left:
                    return TryMoveLeft(piece);
                case Vector2Direction.Right:
                    return TryMoveRight(piece);
            }

            return false;
        }

        /// <summary>
        /// Checks if piece can move right.
        /// </summary>
        /// <param name="piece">Piece to move.</param>
        /// <returns>True if piece can move, False otherwise.</returns>
        private static bool TryMoveRight(Piece piece)
        {
            if (piece.CurrentTile.GridPosition.x + 1 >= s_gridTiles.GetLength(0) ||
                s_gridTiles[piece.CurrentTile.GridPosition.x + 1, piece.CurrentTile.GridPosition.y].IsOccupied == false)
            {
                piece.CantMove(Vector2Direction.Right);
                return false;
            }

            GameGridTile targetTile = s_gridTiles[piece.CurrentTile.GridPosition.x + 1, piece.CurrentTile.GridPosition.y];
            s_animController.AnimateMovePiece(piece, targetTile, Vector2Direction.Right);

            return true;
        }

        /// <summary>
        /// Checks if piece can move left.
        /// </summary>
        /// <param name="piece">Piece to move.</param>
        /// <returns>True if piece can move, False otherwise.</returns>
        private static bool TryMoveLeft(Piece piece)
        {
            if (piece.CurrentTile.GridPosition.x - 1 < 0 ||
                s_gridTiles[piece.CurrentTile.GridPosition.x - 1, piece.CurrentTile.GridPosition.y].IsOccupied == false)
            {
                piece.CantMove(Vector2Direction.Left);
                return false;
            }

            GameGridTile targetTile = s_gridTiles[piece.CurrentTile.GridPosition.x - 1, piece.CurrentTile.GridPosition.y];
            s_animController.AnimateMovePiece(piece, targetTile, Vector2Direction.Left);

            return true;
        }

        /// <summary>
        /// Checks if piece can move down.
        /// </summary>
        /// <param name="piece">Piece to move.</param>
        /// <returns>True if piece can move, False otherwise.</returns>
        private static bool TryMoveDown(Piece piece)
        {
            if (piece.CurrentTile.GridPosition.y - 1 < 0 ||
                s_gameManager.GameGrid.GridTiles[piece.CurrentTile.GridPosition.x, piece.CurrentTile.GridPosition.y - 1].IsOccupied == false)
            {
                piece.CantMove(Vector2Direction.Down);
                return false;
            }

            GameGridTile targetTile = s_gridTiles[piece.CurrentTile.GridPosition.x, piece.CurrentTile.GridPosition.y - 1];
            s_animController.AnimateMovePiece(piece, targetTile, Vector2Direction.Down);

            return true;
        }

        /// <summary>
        /// Checks if piece can move up.
        /// </summary>
        /// <param name="piece">Piece to move.</param>
        /// <returns>True if piece can move, False otherwise.</returns>
        private static bool TryMoveUp(Piece piece)
        {
            if (piece.CurrentTile.GridPosition.y + 1 >= s_gridTiles.GetLength(1) ||
                s_gridTiles[piece.CurrentTile.GridPosition.x, piece.CurrentTile.GridPosition.y + 1].IsOccupied == false)
            {
                piece.CantMove(Vector2Direction.Up);
                return false;
            }

            GameGridTile targetTile = s_gridTiles[piece.CurrentTile.GridPosition.x, piece.CurrentTile.GridPosition.y + 1];
            s_animController.AnimateMovePiece(piece, targetTile, Vector2Direction.Up);

            return true;
        }
    }
}