namespace VUDK.Extensions
{
    using UnityEngine;

    public static class DebugExtension
    {
        public static void Advise(string message)
        {
            Debug.Log($"<color=yellow>{message}</color>");
        }

        public static void Error(string message)
        {
            Debug.LogError($"<color=red>{message}</color>");
        }
    }
}