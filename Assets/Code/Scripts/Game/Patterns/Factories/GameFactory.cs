namespace ProjectH.Patterns.Factories
{
    using ProjectH.Features.Grid.Pieces;
    using ProjectH.Features.Grid.Pieces.Keys;
    using ProjectH.Features.Levels;
    using ProjectH.Managers.Main;
    using UnityEngine;
    using VUDK.Features.Main.ScriptableKeys;
    using VUDK.Generic.Managers.Main;

    /// <summary>
    /// Factory responsible for creating game-related objects.
    /// </summary>
    public static class GameFactory
    {
        /// <summary>
        /// Creates a piece by key.
        /// </summary>
        /// <param name="pieceKey">Piece key.</param>
        /// <param name="levelManager">Level manager.</param>
        /// <returns>Created piece.</returns>
        public static Piece Create(PieceKey pieceKey, LevelManager levelManager)
        {
            if (MainManager.Ins.PoolsManager.Pools[pieceKey].Get().TryGetComponent(out Piece piece))
            {
                levelManager.AddPieceToField(piece);
                return piece;
            }

            return null;
        }
    }
}