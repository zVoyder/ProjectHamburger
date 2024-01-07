namespace ProjectH.Features.Grid
{
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Generic.Structures.Grid;
    using VUDK.Patterns.Initialization.Interfaces;
    using VUDK.Generic.Managers.Main;
    using ProjectH.Features.Grid.Tiles;
    using ProjectH.Features.Levels.Data;
    using ProjectH.Features.Grid.Pieces;
    using ProjectH.Features.Grid.Pieces.Keys;

    public class GameGrid : LayoutGrid<GameGridTile>, IInit<LevelData>
    {
        private LevelData _levelData;

        public void Init(LevelData levelData)
        {
            SetGridLayoutGroup();
            _levelData = levelData;
            SetSize(levelData.LevelGridSize.x, levelData.LevelGridSize.y);
            Init();
        }

        public override void FillGrid()
        {
            for (int y = _levelData.LevelGridSize.y - 1; y >= 0 ; y--)
            {
                for (int x = 0; x < _levelData.LevelGridSize.x; x++)
                {
                    int index = y * _levelData.LevelGridSize.x + x;
                    PieceKey pieceKey = _levelData.PieceKeys[index];

                    if (pieceKey == null) continue;

                    if (MainManager.Ins.PoolsManager.Pools[pieceKey].Get().TryGetComponent(out Piece piece))
                    {
                        piece.SetTile(GridTiles[x, y]);
                        GridTiles[x, y].Insert(piece);
                    }
                }
            }
        }

        private void SetGridLayoutGroup()
        {
            GridLayoutGroup.startCorner = GridLayoutGroup.Corner.LowerLeft;
            GridLayoutGroup.startAxis = GridLayoutGroup.Axis.Horizontal;
            GridLayoutGroup.childAlignment = TextAnchor.LowerLeft;
        }
    }
}