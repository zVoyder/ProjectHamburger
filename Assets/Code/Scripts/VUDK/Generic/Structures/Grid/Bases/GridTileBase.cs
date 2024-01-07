namespace VUDK.Generic.Structures.Grid.Bases
{
    using UnityEngine;

    public abstract class GridTileBase : MonoBehaviour
    {
        public Vector2Int GridPosition { get; private set; }

        public virtual void Init(Vector2Int gridPosition)
        {
            transform.name = $"Tile ( {gridPosition.x} ; {gridPosition.y} )";
            GridPosition = gridPosition;
        }
    }
}
