namespace VUDK.Extensions
{
    using UnityEngine;

    public static class QuaternionExtensions
    {
        public static Vector3 SignedEulerAngles(this Quaternion rotation)
        {
            Vector3 eulerAngles = rotation.eulerAngles;
            eulerAngles.x = rotation.x < 0f ? eulerAngles.x - 360 : eulerAngles.x;
            eulerAngles.y = rotation.y < 0f ? eulerAngles.y - 360 : eulerAngles.y;
            eulerAngles.z = rotation.z < 0f ? eulerAngles.z - 360 : eulerAngles.z;
            return eulerAngles;
        }

        public static Quaternion RoundToFundamentalAngles(Quaternion rotation)
        {
            Vector3 euler = rotation.eulerAngles;

            euler.x = Mathf.Round(euler.x / 90.0f) * 90.0f;
            euler.y = Mathf.Round(euler.y / 90.0f) * 90.0f;
            euler.z = Mathf.Round(euler.z / 90.0f) * 90.0f;

            return Quaternion.Euler(euler);
        }
    }
}
