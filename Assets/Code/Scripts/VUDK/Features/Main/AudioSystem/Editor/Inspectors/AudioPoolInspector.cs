namespace VUDK.Features.Main.AudioSystem.Editor.Inspectors
{
    using UnityEditor;
    using UnityEngine;
    using VUDK.Features.Main.AudioSystem.AudioObjects;
    using static VUDK.Editor.Utility.VUDKEditorUtility;

    [CustomEditor(typeof(AudioPool))]
    public class AudioPoolInspector : Editor
    {
        private AudioPool _audioPool;
        private SerializedProperty _sources;
        private bool _isShowed = true;

        private void OnEnable()
        {
            _audioPool = (AudioPool)target;
            _sources = serializedObject.FindProperty(AudioPool.PropertyNames.SourcesProperty);
        }

        public override void OnInspectorGUI()
        {
            DrawHeaderLabel("Audio Pool");
            DrawButtons();
            EditorGUILayout.Space(5);
            DrawPool();
        }

        private void DrawPool()
        {
            serializedObject.Update();

            if (_sources.arraySize == 0)
            {
                EditorGUILayout.HelpBox("No sources in the pool.", MessageType.Info);
                return;
            }

            DrawFoldout(ref _isShowed, "Audio Sources", () =>
            {
                DrawDisabledGroup(() =>
                {
                    for (int i = 0; i < _sources.arraySize; i++)
                    {
                        var source = _sources.GetArrayElementAtIndex(i);
                        EditorGUILayout.PropertyField(source, new GUIContent("Source " + i), true);
                    }
                });
            });

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawButtons()
        {
            DrawHorizontal(() =>
            {
                if (GUILayout.Button("+"))
                    _audioPool.AddSource();

                if (GUILayout.Button("-"))
                    _audioPool.RemoveSource();
            });

            if (GUILayout.Button("Clear"))
                _audioPool.Reset();
        }
    }
}