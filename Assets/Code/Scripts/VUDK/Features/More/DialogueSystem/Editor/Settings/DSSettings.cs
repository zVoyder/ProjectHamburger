namespace VUDK.Features.More.DialogueSystem.Editor.Settings
{
    using UnityEditor;
    using VUDK.Features.More.DialogueSystem.Editor.Windows;
    using static VUDK.Features.More.DialogueSystem.Editor.Constants.DSEditorPaths;

    public class DSSettings
    {
        private const string Label = "Dialogue System";

        private static string s_customDialoguesSavePath;

        private static bool s_useDefaultPath = !HasCustomDialoguesSavePath();

        [SettingsProvider]
        public static SettingsProvider Settings()
        {
            var provider = new SettingsProvider($"{Constants.DSEditorPaths.DSSettings}/{Label}", SettingsScope.User)
            {
                label = Label,
                guiHandler = (searchContext) =>
                {
                    EditorGUI.BeginChangeCheck();
                    s_useDefaultPath = EditorGUILayout.Toggle("Use Default Save Path", s_useDefaultPath);
                    s_customDialoguesSavePath = DialoguesSaveParentFolderPath;

                    if (s_useDefaultPath)
                    {
                        EditorGUI.BeginDisabledGroup(true);
                        EditorGUILayout.TextField("Dialogues Save Path", DefaultDialoguesDataFolderPath);
                        EditorGUI.EndDisabledGroup();
                    }
                    else
                    {
                        s_customDialoguesSavePath = EditorGUILayout.TextField("Dialogues Save Path", s_customDialoguesSavePath);
                    }

                    if (EditorGUI.EndChangeCheck())
                    {
                        if (s_useDefaultPath)
                            s_customDialoguesSavePath = DefaultDialoguesDataFolderPath;

                        SetDialoguesSavePath(s_customDialoguesSavePath);
                        DSEditorWindow.CloseWindow();
                    }
                },
                keywords = new string[] { "Dialogues", "Save", "Path" }
            };

            return provider;
        }
    }
}
