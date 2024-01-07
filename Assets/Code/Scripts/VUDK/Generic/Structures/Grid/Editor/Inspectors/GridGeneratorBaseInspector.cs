namespace VUDK.Generic.Structures.Grid.Editor.Inspectors
{
    using UnityEditor;
    using UnityEngine;
    using VUDK.Generic.Structures.Grid.Bases;
    using VUDK.Generic.Structures.Grid.Interfaces;

    [CustomEditor(typeof(GridBase<>), true)]
    public class GridGeneratorBaseInspector : Editor
    {
        private IGrid _grid;

        private void OnEnable()
        {
            _grid = target as IGrid;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            GUILayout.Space(4);
            if (GUILayout.Button("Clear Grid")) _grid.ClearGrid();
        }
    }
}
