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
        public static Piece Create(PieceKey pieceKey, LevelManager levelManager)
        {
            if (MainManager.Ins.PoolsManager.Pools[pieceKey].Get().TryGetComponent(out Piece piece))
            {
                levelManager.AddPiece(piece);
                return piece;
            }

            return null;
        }
    }
}