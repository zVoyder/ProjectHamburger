namespace VUDK.Editor.Tools.Debug
{
    using UnityEditor;
    using UnityEngine;

    public class TimeSettings : EditorWindow
    {
        private int _targetFPS = 60;
        private float _targetTimeScale = 1f;

        [MenuItem("VUDK/Debug/Time Settings")]
        public static void ShowWindow()
        {
            GetWindow(typeof(TimeSettings), false, "Time Settings");
        }

        private void OnGUI()
        {
            GUILayout.Label("Time Settings", EditorStyles.boldLabel);

            _targetFPS = EditorGUILayout.IntField("Target FPS", _targetFPS);

            if (GUILayout.Button("Lock"))
                SetFPS(_targetFPS);
            if (GUILayout.Button("Unlock"))
                UnlockFPS();

            _targetTimeScale = EditorGUILayout.FloatField("Target TimeScale", _targetTimeScale);

            if (GUILayout.Button("Set"))
                SetTimeScale(_targetTimeScale);
            if (GUILayout.Button("Reset"))
                SetTimeScale(1f);

            EditorGUILayout.Space();
            EditorGUILayout.HelpBox("Debug tool that allows you to control the frame rate and time scale of the game in play mode. Lock or unlock the frame rate, and set or reset the time scale as needed.", MessageType.Info);
        }

        private void SetFPS(int fps)
        {
            Application.targetFrameRate = fps;
            Debug.Log("FPS Locked to: " + fps);
        }

        private void SetTimeScale(float timeScale)
        {
            Time.timeScale = timeScale;

            Debug.Log("Time Scale setted to: " + timeScale);
        }

        private void UnlockFPS()
        {
            Application.targetFrameRate = 0;
            Debug.Log("FPS Unlocked");
        }
    }
}
