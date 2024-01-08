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

        /// <summary>
        /// Set current tile.
        /// </summary>
        /// <param name="tile">Tile to set.</param>
        public void SetTile(GameGridTile tile)
        {
            CurrentTile = tile;
        }

        /// <summary>
        /// Place piece in tile.
        /// </summary>
        /// <param name="tile">Tile to place.</param>
        public void PlaceInTile(GameGridTile tile)
        {
            GraphicsController.PlayPlacedAnimation();
            SetTile(tile);
        }

        /// <summary>
        /// Stack transform piece on tile.
        /// </summary>
        /// <param name="tile">Specified tile.</param>
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

        /// <summary>
        /// Triggers piece can't move.
        /// </summary>
        /// <param name="direction"> Specified direction.</param>
        public void CantMove(Vector2Direction direction)
        {
            GraphicsController.PlayCantMove(direction);
        }

        /// <inheritdoc />
        public override void Clear()
        {
            base.Clear();
            transform.ResetTransform(false);
        }
    }
}
