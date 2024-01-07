namespace VUDK.Features.More.DialogueSystem.Editor.Constants
{
    using System.IO;
    using UnityEditor;
    using UnityEngine;

    public static class DSEditorPaths
    {
        #region FIXED PATHS

        public const string DefaultDialoguesDataFolderPath = "Assets/Code/Scripts/VUDK/Features/More/DialogueSystem";
        public static string EditorFolderPath = "";

        #endregion FIXED PATHS

        #region FOLDER NAMES

        public const string DialoguesDataFolderName = "DialoguesData";
        public const string DialoguesNodesFolderName = "DialogueNodes";

        #endregion FOLDER NAMES

        #region SETTINGS PATHS

        public const string DSSettings = "Preferences/VUDK";

        #endregion SETTINGS PATHS

        #region EDITOR PREFS

        private const string DialoguesSavePathPrefs = "DS_ParentFolderPath";

        #endregion EDITOR PREFS

        #region EDITOR PROPERTY PATHS

        public static string GraphsAssetPath => $"{EditorFolderPath}/Graphs";
        public static string EditorResourcesPath => $"{EditorFolderPath}/Resources";
        public static string EditorIconsPath => $"{EditorResourcesPath}/EditorIcons";
        public static string StyleSheetsPath => $"{EditorResourcesPath}/Styles";

        #endregion EDITOR PROPERTY PATHS

        #region ASSET PROPERTY PATHS

        public static string MainFolderDialoguesDataPath => $"{DialoguesSaveParentFolderPath}/{DialoguesDataFolderName}";
        public static string DialoguesDataFolderPath => $"{MainFolderDialoguesDataPath}/AllDialogues";
        public static string DialoguesActorsAssetPath => $"{MainFolderDialoguesDataPath}/Actors";

        #endregion ASSET PROPERTY PATHS

        static DSEditorPaths()
        {
            EditorFolderPath = GetEditorFolderAssetPath();
        }

        public static string DialoguesSaveParentFolderPath => EditorPrefs.GetString(DialoguesSavePathPrefs, DefaultDialoguesDataFolderPath);

        public static void SetDialoguesSavePath(string path)
        {
            EditorPrefs.SetString(DialoguesSavePathPrefs, path);
        }

        public static bool HasCustomDialoguesSavePath()
        {
            return DialoguesSaveParentFolderPath != DefaultDialoguesDataFolderPath;
        }

        private static string GetEditorFolderAssetPath([System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "")
        {
            string editorPath = "";
            string sourcePath = Path.GetDirectoryName(sourceFilePath);
            string directory = $"{nameof(Constants)}";
            string projectPath = Path.GetDirectoryName(Application.dataPath);
            char separator = Path.DirectorySeparatorChar;

            editorPath = sourcePath.Replace(projectPath, "");
            editorPath = editorPath.Replace(directory, "");
            editorPath = editorPath.TrimStart(separator);
            editorPath = editorPath.TrimEnd(separator);

            return editorPath;
        }
    }
}