namespace ProjectH.Features.Grid.Tiles
{
    using System.Collections.Generic;
    using UnityEngine;
    using VUDK.Generic.Structures.Grid.Bases;
    using ProjectH.Features.Grid.Pieces;
    using VUDK.Extensions;

    [RequireComponent(typeof(Collider))]
    public class GameGridTile : GridTileBase
    {
        private Collider _collider;

        public List<Piece> Stack { get; private set; }
        public bool IsOccupied => Stack.Count > 0;
        public int StackCount => Stack.Count;
        public Piece TopPiece => Stack.Count > 0 ? Stack[Stack.Count - 1] : null;
        public Piece BottomPiece => Stack.Count > 0 ? Stack[0] : null;

        private void Awake()
        {
            TryGetComponent(out _collider);
            Stack = new List<Piece>();
        }

        public void Insert(Piece piece)
        {
            Stack.Add(piece);
            Vector3 scale = piece.transform.lossyScale;
            piece.transform.SetParent(transform, false);
            piece.transform.SetLossyScale(scale);
        }

        public void AddToStack(Piece piece)
        {
            Stack.Add(piece);
        }

        public void AddToStack(List<Piece> pieces)
        {
            Stack.AddRange(pieces);
        }

        public void RemoveFromStack(Piece piece)
        {
            Stack.Remove(piece);
        }

        public void RemoveFromStack(List<Piece> pieces)
        {
            foreach (Piece piece in pieces)
                Stack.Remove(piece);
        }

        public void ClearStack()
        {
            Stack.Clear();
        }

        public Vector3 GetSouthPosition(Vector3 offset = default)
        {
            return transform.position + new Vector3(0f, 0f, -_collider.bounds.extents.z) + offset;
        }

        public Vector3 GetNorthPosition(Vector3 offset = default)
        {
            return transform.position + new Vector3(0f, 0f, _collider.bounds.extents.z) + offset;
        }

        public Vector3 GetEastPosition(Vector3 offset = default)
        {
            return transform.position + new Vector3(_collider.bounds.extents.x, 0f, 0f) + offset;
        }

        public Vector3 GetWestPosition(Vector3 offset = default)
        {
            return transform.position + new Vector3(-_collider.bounds.extents.x, 0f, 0f) + offset;
        }
    }
}