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

        /// <summary>
        /// Insert piece in stack.
        /// </summary>
        /// <param name="piece">Piece to insert.</param>
        public void Insert(Piece piece)
        {
            Stack.Add(piece);
            Vector3 scale = piece.transform.lossyScale;
            piece.transform.SetParent(transform, false);
            piece.transform.SetLossyScale(scale);
        }

        /// <summary>
        /// Adds piece to stack list.
        /// </summary>
        /// <param name="piece">Piece to add to the list.</param>
        public void AddToStack(Piece piece)
        {
            Stack.Add(piece);
        }

        /// <summary>
        /// Adds pieces to stack list
        /// </summary>
        /// <param name="pieces">Pieces to add to the list.</param>
        public void AddToStack(List<Piece> pieces)
        {
            Stack.AddRange(pieces);
        }

        /// <summary>
        /// Removes piece from stack list.
        /// </summary>
        /// <param name="piece">Piece to remove from the list.</param>
        public void RemoveFromStack(Piece piece)
        {
            Stack.Remove(piece);
        }

        /// <summary>
        /// Removes pieces from stack list.
        /// </summary>
        /// <param name="pieces">Pieces to remove from the list.</param>
        public void RemoveFromStack(List<Piece> pieces)
        {
            foreach (Piece piece in pieces)
                Stack.Remove(piece);
        }

        /// <summary>
        /// Clears stack list.
        /// </summary>
        public void ClearStack()
        {
            Stack.Clear();
        }

        /// <summary>
        /// Gets bounds south side position.
        /// </summary>
        /// <param name="offset">Offset to apply.</param>
        /// <returns>South side position.</returns>
        public Vector3 GetSouthPosition(Vector3 offset = default)
        {
            return transform.position + new Vector3(0f, 0f, -_collider.bounds.extents.z) + offset;
        }

        /// <summary>
        /// Gets bounds north side position.
        /// </summary>
        /// <param name="offset"> Offset to apply.</param>
        /// <returns> North side position.</returns>
        public Vector3 GetNorthPosition(Vector3 offset = default)
        {
            return transform.position + new Vector3(0f, 0f, _collider.bounds.extents.z) + offset;
        }

        /// <summary>
        /// Gets bounds east side position.
        /// </summary>
        /// <param name="offset"> Offset to apply.</param>
        /// <returns> East side position.</returns>
        public Vector3 GetEastPosition(Vector3 offset = default)
        {
            return transform.position + new Vector3(_collider.bounds.extents.x, 0f, 0f) + offset;
        }

        /// <summary>
        /// Gets bounds west side position.
        /// </summary>
        /// <param name="offset"> Offset to apply.</param>
        /// <returns> West side position.</returns>
        public Vector3 GetWestPosition(Vector3 offset = default)
        {
            return transform.position + new Vector3(-_collider.bounds.extents.x, 0f, 0f) + offset;
        }
    }
}