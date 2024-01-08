namespace VUDK.Features.Main.AudioSystem.Editor.Inspectors
{
    using UnityEditor;
    using UnityEngine;
    using VUDK.Features.Main.AudioSystem.AudioObjects;
    using VUDK.Features.More.DialogueSystem.Editor.Utilities;
    using static VUDK.Editor.Utility.VUDKEditorUtility;

    [CustomEditor(typeof(AudioPool))]
    public class AudioPoolInspector : Editor
    {
        private AudioPool _audioPool;
        private SerializedProperty _sourcesProperty;

        private bool _isShowed = true;

        private void OnEnable()
        {
            _audioPool = (AudioPool)target;
            _sourcesProperty = serializedObject.FindProperty(AudioPool.PropertyNames.SourcesProperty);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawHeaderLabel("Audio Pool");
            DrawButtons();
            EditorGUILayout.Space(5);
            DrawPool();
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawPool()
        {
            serializedObject.Update();

            if (_sourcesProperty.arraySize == 0)
            {
                EditorGUILayout.HelpBox("No sources in the pool.", MessageType.Info);
                return;
            }

            DrawFoldout(ref _isShowed, "Audio Sources", () =>
            {
                DrawDisabledGroup(() =>
                {
                    for (int i = 0; i < _sourcesProperty.arraySize; i++)
                    {
                        var source = _sourcesProperty.GetArrayElementAtIndex(i);
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