namespace VUDK.Extensions
{
    using System.Collections.Generic;
    using UnityEngine;

    public static class MathExtension
    {
        /// <summary>
        /// Gets the percent of the number based on the given max number
        /// </summary>
        /// <param name="n">number</param>
        /// <param name="max">max number</param>
        /// <returns>percentage</returns>
        public static float AsPercentOf(this float n, float max)
        {
            return (n / max) * 100f;
        }

        /// <summary>
        /// Allows to see if a number is approximately the same compared with an other number.
        /// </summary>
        /// <param name="a">This number.</param>
        /// <param name="b">Other number.</param>
        /// <param name="tollerance">Tollerance Threshold.</param>
        /// <returns>True if they are approximately the same, False if not.</returns>
        public static bool IsApproximatelyEqual(this float n, float toNumber, float tolerance = 0.01f)
        {
            float difference = Mathf.Abs(n - toNumber);
            return difference < tolerance;
        }

        /// <summary>
        /// Gets the closest multiple angle of a given angle in degree.
        /// </summary>
        /// <param name="angle">Given angle in degree.</param>
        /// <param name="multiple">Multiple</param>
        /// <returns>Closest multiple angle.</returns>
        public static float GetClosestMultipleAngle(this float angle, float multiple)
        {
            return Mathf.Round(angle / multiple) * multiple;
        }

        public static Vector3[] GenerateBezierSpline(Vector3[] points, int res)
        {
            List<Vector3> splinePoints = new List<Vector3>();

            for (int i = 0; i < points.Length - 3; i += 3)
            {
                Vector3 p0 = points[i];
                Vector3 p1 = points[i + 1];
                Vector3 p2 = points[i + 2];
                Vector3 p3 = points[i + 3];

                for (float t = 0; t <= 1; t += 1f / res)
                {
                    float u = 1 - t;
                    float tt = t * t;
                    float uu = u * u;
                    float uuu = uu * u;
                    float ttt = tt * t;

                    Vector3 currentPoint = uuu * p0 + 3 * uu * t * p1 + 3 * u * tt * p2 + ttt * p3;
                    splinePoints.Add(currentPoint);
                }
            }

            return splinePoints.ToArray();
        }

        public static Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            Vector3 p = uuu * p0; // (1-t)^3 * P0
            p += 3 * uu * t * p1; // 3(1-t)^2 * t * P1
            p += 3 * u * tt * p2; // 3(1-t) * t^2 * P2
            p += ttt * p3; // t^3 * P3

            return p;
        }

        public static Vector3[] GenerateCatmullRomSpline(Transform[] points, int res)
        {
            List<Vector3> splinePoints = new List<Vector3>();

            for (int i = 0; i < points.Length - 1; i++)
            {
                for (float t = 0; t <= 1; t += 1f / res)
                {
                    Vector3 p0 = GetCatmullPoint(points, i - 1);
                    Vector3 p1 = points[i].position;
                    Vector3 p2 = points[i + 1].position;
                    Vector3 p3 = GetCatmullPoint(points, i + 2);

                    float t2 = t * t;
                    float t3 = t2 * t;

                    Vector3 a = -0.5f * p0 + 1.5f * p1 - 1.5f * p2 + 0.5f * p3;
                    Vector3 b = p0 - 2.5f * p1 + 2 * p2 - 0.5f * p3;
                    Vector3 c = -0.5f * p0 + 0.5f * p2;
                    Vector3 d = p1;

                    Vector3 position = a * t3 + b * t2 + c * t + d;
                    splinePoints.Add(position);
                }
            }

            return splinePoints.ToArray();
        }

        private static Vector3 GetCatmullPoint(Transform[] points, int index)
        {
            if (index < 0)
            {
                return points[0].position;
            }
            else if (index >= points.Length)
            {
                return points[points.Length - 1].position;
            }
            else
            {
                return points[index].position;
            }
        }
    }
}