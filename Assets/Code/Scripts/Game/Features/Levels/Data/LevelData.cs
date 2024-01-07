namespace ProjectH.Features.Levels.Data
{
    using UnityEngine;
    using ProjectH.Features.Grid.Pieces.Keys;

    [CreateAssetMenu(fileName = "LevelData", menuName = "Game/Level")]
    public class LevelData : ScriptableObject
    {
        [Min(2)]
        public Vector2Int LevelGridSize;
        public PieceKey[] PieceKeys;

        public static class PropertyNames
        {
            public static string LevelGridSize => nameof(LevelGridSize);
        }
    }
}
