namespace VUDK.Features.More.DialogueSystem.Editor.Windows
{
    using System.IO;
    using UnityEditor;
    using UnityEditor.UIElements;
    using UnityEngine;
    using UnityEngine.UIElements;
    using VUDK.Extensions;
    using VUDK.Features.More.DialogueSystem.Editor.Constants;
    using VUDK.Features.More.DialogueSystem.Editor.Utilities;

    public class DSEditorWindow : EditorWindow
    {
        private readonly string _defaultFileName = "New Dialogue Graph";

        private static TextField s_fileNameTextField;
        private static DSEditorWindow s_window;

        private DSGraphView _graphView;
        private Button _saveButton;
        private Button _loadButton;
        private Button _miniMapButton;

        private void OnEnable()
        {
            AddGraphView();
            AddToolbar();
            AddStyles();
        }

        [MenuItem("VUDK/Editors/Dialogue Graph")]
        public static void OpenWindow()
        {
            s_window = GetWindow<DSEditorWindow>();
            SetTitleContent(s_window);
            DSIOUtility.CreateMainFolders();
        }

        public static void CloseWindow()
        {
            s_window?.Close();
        }

        private static void SetTitleContent(DSEditorWindow window)
        {
            Texture2D text = DSIOUtility.LoadIcon("ico_editor.png");
            if (text == null)
                Debug.LogError("Icon not found");
            window.titleContent = new GUIContent("Dialogue Graph", text);
        }

        public static void UpdateFileName(string newFileName)
        {
            s_fileNameTextField.value = newFileName;
        }

        public void EnableSaving()
        {
            _saveButton.SetEnabled(true);
        }

        public void DisableSaving()
        {
            _saveButton.SetEnabled(false);
        }

        private void AddGraphView()
        {
            _graphView = new DSGraphView(this);

            _graphView.StretchToParentSize();
            rootVisualElement.Add(_graphView);
        }

        private void AddToolbar()
        {
            Toolbar toolbar = new Toolbar();

            s_fileNameTextField = DSElementUtility.CreateTextField(_defaultFileName, "File Name:", callback =>
            {
                s_fileNameTextField.value = callback.newValue.RemoveSpecialAndWhitespaces();
            });

            _saveButton = DSElementUtility.CreateButton("Save", () => Save());
            _loadButton = DSElementUtility.CreateButton("Load", () => Load());
            _miniMapButton = DSElementUtility.CreateButton("Mini Map", () => ToggleMiniMap());
            Button clearButton = DSElementUtility.CreateButton("Clear", () => Clear());
            Button resetButton = DSElementUtility.CreateButton("Reset", () => ResetGraph());
            Button actorButton = DSElementUtility.CreateButton("Create Actor", () => DSActorWindow.Open());

            toolbar.Add(s_fileNameTextField);
            toolbar.Add(_saveButton);
            toolbar.Add(_loadButton);
            toolbar.Add(clearButton);
            toolbar.Add(resetButton);
            toolbar.Add(_miniMapButton);
            toolbar.Add(actorButton);
            toolbar.AddStyleSheets("DSToolbarStyles.uss");

            rootVisualElement.Add(toolbar);
        }

        private void Clear()
        {
            _graphView.ClearGraph();
        }

        private void ResetGraph()
        {
            Clear();
            UpdateFileName(_defaultFileName);
        }

        private void AddStyles()
        {
            rootVisualElement.AddStyleSheets("DSVariables.uss");
        }

        private void Save()
        {
            if (string.IsNullOrEmpty(s_fileNameTextField.value))
            {
                EditorUtility.DisplayDialog(
                    "Invalid file name",
                    "Please enter a valid file name.",
                    "OK"
                    );

                return;
            }

            DSIOUtility.Init(_graphView, s_fileNameTextField.value);
            DSIOUtility.Save();
        }

        private void Load()
        {
            string filePath = EditorUtility.OpenFilePanel("Load Dialogue Graph", $"{DSEditorPaths.GraphsAssetPath}", "asset");

            if (string.IsNullOrEmpty(filePath)) return;

            Clear();
            DSIOUtility.Init(_graphView, Path.GetFileNameWithoutExtension(filePath));
            DSIOUtility.Load();
        }

        private void ToggleMiniMap()
        {
            _graphView.ToggleMiniMap();
            _miniMapButton.ToggleInClassList("ds-toolbar__button__selected");
        }
    }
}