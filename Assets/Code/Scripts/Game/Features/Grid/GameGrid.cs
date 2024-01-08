namespace ProjectH.Features.Grid
{
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Generic.Structures.Grid;
    using VUDK.Patterns.Initialization.Interfaces;
    using VUDK.Generic.Managers.Main.Interfaces;
    using VUDK.Generic.Managers.Main;
    using ProjectH.Features.Grid.Tiles;
    using ProjectH.Features.Levels.Data;
    using ProjectH.Features.Grid.Pieces;
    using ProjectH.Features.Grid.Pieces.Keys;
    using ProjectH.Patterns.Factories;
    using ProjectH.Managers.Main;

    public class GameGrid : LayoutGrid<GameGridTile>, IInit<LevelData>, ICastGameManager<GameManager>
    {
        private LevelData _levelData;

        public GameManager GameManager => MainManager.Ins.GameManager as GameManager;

        protected override void Awake()
        {
            base.Awake();
            SetGridLayoutGroup();
        }

        /// <inheritdoc />
        public void Init(LevelData levelData)
        {
            _levelData = levelData;
            SetSize(levelData.LevelGridSize.x, levelData.LevelGridSize.y);
            Init();
            FillGrid();
        }

        /// <inheritdoc />
        public override void FillGrid()
        {
            for (int y = _levelData.LevelGridSize.y - 1; y >= 0 ; y--)
            {
                for (int x = 0; x < _levelData.LevelGridSize.x; x++)
                {
                    int index = y * _levelData.LevelGridSize.x + x;
                    PieceKey pieceKey = _levelData.PieceKeys[index];

                    if (pieceKey == null) continue;

                    Piece piece = GameFactory.Create(pieceKey, GameManager.LevelManager);
                    piece.SetTile(GridTiles[x, y]);
                    GridTiles[x, y].Insert(piece);
                }
            }
        }

        /// <summary>
        /// Set grid layout group settings.
        /// </summary>
        private void SetGridLayoutGroup()
        {
            GridLayoutGroup.startCorner = GridLayoutGroup.Corner.LowerLeft;
            GridLayoutGroup.startAxis = GridLayoutGroup.Axis.Horizontal;
            GridLayoutGroup.childAlignment = TextAnchor.LowerLeft;
        }
    }
}