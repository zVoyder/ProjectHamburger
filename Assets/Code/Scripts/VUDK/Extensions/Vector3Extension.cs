namespace VUDK.Extensions
{
    using UnityEngine;

    public enum Vector3Direction
    {
        Up,
        Down,
        Left,
        Right,
        Forward,
        Backward
    }

    public static class Vector3Extension
    {
        public static Vector3 GetDirection (this Vector3Direction direction)
        {
            switch (direction)
            {
                case Vector3Direction.Up:
                    return Vector3.up;

                case Vector3Direction.Down:
                    return Vector3.down;

                case Vector3Direction.Left:
                    return Vector3.left;

                case Vector3Direction.Right:
                    return Vector3.right;

                case Vector3Direction.Forward:
                    return Vector3.forward;
                
                case Vector3Direction.Backward:
                    return Vector3.back;
            }

            return Vector3.zero;
        }

        public static float CalculateDistance(this Vector3 v1, Vector3 v2)
        {
            return Mathf.Sqrt(
                Mathf.Pow(v1.x - v2.x, 2) +
                Mathf.Pow(v1.y - v2.y, 2) +
                Mathf.Pow(v1.z - v2.z, 2)
                );
        }

        public static float Module(this Vector3 vector)
        {
            return Mathf.Pow(vector.x, 2) + Mathf.Pow(vector.y, 2) + Mathf.Pow(vector.z, 2);
        }

        /// <summary>
        /// Sums to the coordinates of the vector to a number.
        /// </summary>
        /// <param name="vector">Vector.</param>
        /// <param name="n">number to sum.</param>
        /// <returns>Vector2 summed with n.</returns>
        public static Vector3 Sum(this Vector3 vector, float n)
        {
            return new Vector3(vector.x + n, vector.y + n, vector.z + n);
        }

        public static Vector3 Sum(this Vector3 vector, float x = 0, float y = 0, float z = 0)
        {
            return new Vector3(vector.x + x, vector.y + y, vector.z + z);
        }

        public static Vector3 Product(this Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
        }

        public static void Components(this Vector3 v1, out Vector3 vx, out Vector3 vy, out Vector3 vz)
        {
            vx = new Vector3(v1.x, 0, 0);
            vy = new Vector3(0, v1.y, 0);
            vz = new Vector3(0, 0, v1.z);
        }

        public static bool IsApproximatelyEqual(this Vector3 v1, Vector3 v2, float tolerance = 0.01f)
        {
            return 
                v1.x.IsApproximatelyEqual(v2.x, tolerance) &&
                v1.y.IsApproximatelyEqual(v2.y, tolerance) &&
                v1.z.IsApproximatelyEqual(v2.z, tolerance);
        }

        public static Vector3 SwapXY(this Vector3 v)
        {
            return new Vector3(v.y, v.x, v.z);
        }

        public static Vector3 SwapXZ(this Vector3 v)
        {
            return new Vector3(v.z, v.y, v.x);
        }

        public static Vector3 SwapYZ(this Vector3 v)
        {
            return new Vector3(v.x, v.z, v.y);
        }
    }
}