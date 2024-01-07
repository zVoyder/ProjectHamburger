namespace VUDK.Extensions
{
    using UnityEngine;

    public enum Vector2Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public static class Vector2Extension
    {
        public static Vector2 GetDirection(this Vector2Direction direction)
        {
            switch (direction)
            {
                case Vector2Direction.Up:
                    return Vector3.up;

                case Vector2Direction.Down:
                    return Vector3.down;

                case Vector2Direction.Left:
                    return Vector3.left;

                case Vector2Direction.Right:
                    return Vector3.right;
            }

            return Vector3.zero;
        }

        /// <summary>
        /// Inverts the coordinates of the vector.
        /// </summary>
        /// <param name="v">Vector.</param>
        /// <returns>Vector2 of inverted coordinates.</returns>
        public static Vector2 Invert(this Vector2 v)
        {
            return new Vector2(v.y, v.x);
        }

        /// <summary>
        /// Sums to the coordinates of the vector a number.
        /// </summary>
        /// <param name="vector">Vector.</param>
        /// <param name="n">number to sum.</param>
        /// <returns>Vector2 summed with n.</returns>
        public static Vector2 Sum(this Vector2 vector, float n)
        {
            return new Vector2(vector.x + n, vector.y + n);
        }
    }
}