namespace ProjectH.Features.Levels.Editor
{
    using UnityEditor;
    using VUDK.Editor.Utility;
    using ProjectH.Features.Grid.Pieces.Keys;
    using ProjectH.Features.Levels.Data;
    using VUDK.Features.More.DialogueSystem.Editor.Utilities;
    using UnityEngine;

    [CustomEditor(typeof(LevelData))]
    public class LevelDataInspector : Editor
    {
        private SerializedProperty _gridSizeProperty;

        private LevelData Target => target as LevelData;

        private void OnEnable()
        {
            _gridSizeProperty = serializedObject.FindProperty(LevelData.PropertyNames.LevelGridSize);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            _gridSizeProperty.DrawPropertyField();
            DrawLevelMatrixEditor();

            if (GUILayout.Button("Clear"))
            {
                for (int i = 0; i < Target.PieceKeys.Length; i++)
                    Target.PieceKeys[i] = null;
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawLevelMatrixEditor()
        {
            VUDKEditorUtility.DrawBox(Color.black, () =>
            {
                EditorGUILayout.LabelField("Level Matrix");

                int rows = Target.LevelGridSize.y;
                int columns = Target.LevelGridSize.x;

                if (Target.PieceKeys == null || Target.PieceKeys.Length != rows * columns)
                    Target.PieceKeys = new PieceKey[columns * rows];

                for (int y = rows - 1; y >= 0; y--)
                {
                    VUDKEditorUtility.DrawHorizontal(() =>
                    {
                        for (int x = 0; x < columns; x++)
                        {
                            EditorGUI.BeginChangeCheck();

                            int index = y * columns + x;
                            Target.PieceKeys[index] = EditorGUILayout.ObjectField(Target.PieceKeys[index], typeof(PieceKey), true) as PieceKey;

                            //Debug.Log("Position at: " + x + " " + y + " " + Target.LevelPieces[index]);

                            if (EditorGUI.EndChangeCheck())
                                EditorUtility.SetDirty(Target);
                        }
                    });
                }
            });
        }
    }
}