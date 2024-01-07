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

        private PieceGraphicsController _graphicsController;

        public GameGridTile CurrentTile { get; private set; }

        private void Awake()
        {
            TryGetComponent(out _graphicsController);
            _graphicsController.Init(this);
        }

        public void SetTile(GameGridTile tile)
        {
            CurrentTile = tile;
        }

        public void PlaceInTile(GameGridTile tile)
        {
            _graphicsController.PlayPlacedAnimation();
            SetTile(tile);
        }

        public void CantMove(Vector2Direction direction)
        {
            _graphicsController.PlayCantMove(direction);
        }

        public override void Clear()
        {
            base.Clear();
            transform.ResetTransform(false);
        }
    }
}
