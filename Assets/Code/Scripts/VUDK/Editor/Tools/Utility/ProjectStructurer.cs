namespace VUDK.Editor.Tools.Utility
{
    using System.IO;
    using UnityEditor;
    using UnityEngine;

    public class ProjectStructurer : EditorWindow
    {
        private const string MainFolder = "Assets";

        private readonly string[] _mainFolders = new string[]
        {
                "Art",
                "Audio",
                "Code",
                "Docs",
                "Level",
                "Resources"
        };

        private readonly string[] _artFolders = new string[]
        {
                "Fonts",
                "Animations",
                "Materials",
                "Models",
                "Sprites",
                "Textures"
        };

        private readonly string[] _audioFolders = new string[]
        {
                "Music",
                "Mixers",
                "SFX"
        };

        private readonly string[] _codeFolders = new string[]
        {
                "Scripts",
                "Shaders",
        };

        private readonly string[] _levelFolders = new string[]
        {
                "Data",
                "Prefabs",
                "Scenes"
        };

        private readonly string[] _resourceFolders = new string[]
        {
                "Settings",
                "Volumes",
                "Third Party"
        };

        [MenuItem("VUDK/Utility/Project Structurer")]
        public static void OpenWindow()
        {
            GetWindow(typeof(ProjectStructurer), false, "Project Structurer");
        }

        private void OnGUI()
        {
            GUILayout.Label("Project Structurer", EditorStyles.boldLabel);
            if (GUILayout.Button("Create Folders"))
                CreateFolders();

            EditorGUILayout.Space();
            EditorGUILayout.HelpBox("Tool that helps organize your Unity project by creating a predefined folder structure. Click 'Create Project Folders' to generate the specified folders in the 'Assets' directory.", MessageType.Info);
        }

        private void CreateFolders()
        {
            CreateFoldersInParentFolder(MainFolder, _mainFolders);
            CreateFoldersInParentFolder(MainFolder + "\\" + _mainFolders[0], _artFolders);
            CreateFoldersInParentFolder(MainFolder + "\\" + _mainFolders[1], _audioFolders);
            CreateFoldersInParentFolder(MainFolder + "\\" + _mainFolders[2], _codeFolders);
            CreateFoldersInParentFolder(MainFolder + "\\" + _mainFolders[4], _levelFolders);
            CreateFoldersInParentFolder(MainFolder + "\\" + _mainFolders[5], _resourceFolders);
        }

        private void CreateFoldersInParentFolder(string parentFolder, string[] folders)
        {
            foreach (string folderName in folders)
            {
                string folderPath = Path.Combine(parentFolder, folderName);

                if (!Directory.Exists(folderPath))
                {
                    AssetDatabase.CreateFolder(parentFolder, folderName);
#if UNITY_EDITOR
                    Debug.Log("Directory created: " + folderPath);
#endif
                }
            }
        }
    }
}