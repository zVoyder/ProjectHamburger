namespace ProjectH.Features.Grid.Pieces
{
    using UnityEngine;
    using VUDK.Extensions;
    using VUDK.Patterns.Pooling;
    using ProjectH.Features.Grid.Tiles;

    [RequireComponent(typeof(PieceGraphicsController))]
    public class Piece : PooledObject
    {
        [field: SerializeField]
        public PieceType PieceType { get; private set; }

        public PieceGraphicsController GraphicsController { get; private set; }
        public GameGridTile CurrentTile { get; private set; }

        private void Awake()
        {
            TryGetComponent(out PieceGraphicsController graphicsController);
            GraphicsController = graphicsController;
            GraphicsController.Init(this);
        }

        public void SetTile(GameGridTile tile)
        {
            CurrentTile = tile;
        }

        public void PlaceInTile(GameGridTile tile)
        {
            GraphicsController.PlayPlacedAnimation();
            SetTile(tile);
        }

        public void StackOnTile(GameGridTile tile)
        {
            if(tile.StackCount > 0)
            {
                transform.SetParent(tile.TopPiece.transform);
                CurrentTile.TopPiece.GraphicsController.PlayStackedEffect();
            }
            else
            {
                GraphicsController.PlayStackedEffect();
                transform.SetParent(tile.transform);
            }
        }

        public void CantMove(Vector2Direction direction)
        {
            GraphicsController.PlayCantMove(direction);
        }

        public override void Clear()
        {
            base.Clear();
            transform.ResetTransform(false);
        }
    }
}
