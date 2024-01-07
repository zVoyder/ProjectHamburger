namespace VUDK.Generic.Structures.Grid
{
    using UnityEngine;
    using VUDK.Extensions;
    using VUDK.Generic.Structures.Grid.Bases;

    public abstract class WorldGrid<T> : GridBase<T> where T : GridTileBase
    {
        public Vector2Int WorldToGridPosition(Vector3 worldPosition)
        {
            Vector3 localPosition = worldPosition - transform.position;
            int x = Mathf.RoundToInt(localPosition.x);
            int y = Mathf.RoundToInt(localPosition.y);

            x = Mathf.Clamp(x, 0, Size.x - 1);
            y = Mathf.Clamp(y, 0, Size.y - 1);

            return new Vector2Int(x, y);
        }

        protected override void InitTile(T tile, Vector2Int gridPosition)
        {
            base.InitTile(tile, gridPosition);

            Vector3 gridPos = new Vector3(gridPosition.x, gridPosition.y, 0f);
            tile.transform.position = transform.position + gridPos;
        }

#if UNITY_EDITOR
        protected override void DrawBounds()
        {
            Gizmos.color = Color.yellow;
            GizmosExtension.DrawWireCube(transform.position, new Vector3(Size.x, Size.y, 0), transform.rotation);
        }
#endif
    }
}