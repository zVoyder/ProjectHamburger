namespace ProjectH.Features.Moves.Undo
{
    using UnityEngine;
    using ProjectH.Features.Grid.Tiles;
    using ProjectH.Features.Grid.Pieces;
    using System.Collections.Generic;

    public struct UndoMove
    {
        public Piece UndoPiece { get; private set; }
        public GameGridTile TargetTile { get; private set; }
        public List<Piece> OldStack { get; private set; }
        public Vector3 RotationPoint { get; private set; }
        public Vector3 AxisDirection { get; private set; }
        
        public UndoMove(Piece undoPiece, List<Piece> oldStack, GameGridTile targetTile, Vector3 rotationPoint, Vector3 axisDirection)
        {
            UndoPiece = undoPiece;
            OldStack = new List<Piece>(oldStack);
            OldStack.Reverse();
            TargetTile = targetTile;
            RotationPoint = rotationPoint;
            AxisDirection = axisDirection;
        }
    }
}