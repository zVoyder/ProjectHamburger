namespace VUDK.Features.Main.SaveSystem.Editor.Settings
{
    using UnityEditor;
    using UnityEngine;
    using VUDK.Editor.Constants;
    using VUDK.Editor.Utility;
    using VUDK.Features.Main.SaveSystem.Utility;

    public class SaveSystemSettings
    {
        private const string Label = "Save System";

        [SettingsProvider]
        public static SettingsProvider Settings()
        {
            var provider = new SettingsProvider($"{EditorConstants.VUDKPrefSettings}/{Label}", SettingsScope.User)
            {
                label = Label,
                guiHandler = (searchContext) =>
                {
                    DrawAllSaves();
                    VUDKEditorUtility.DrawSpace();

                    if (GUILayout.Button("Delete All Saves"))
                        SaveManager.DeleteAllSaves();
                },
                keywords = new string[] { "Save", "Data", "File" }
            };

            return provider;
        }

        private static void DrawAllSaves()
        {
            VUDKEditorUtility.DrawHeaderLabel("Current All File Saves");
            string[] files = SaveUtility.GetFileNames(".bin", true);

            if (files.Length == 0)
            {
                EditorGUILayout.HelpBox("No saves found.", MessageType.Info);
                return;
            }

            VUDKEditorUtility.DrawIndented(1, () =>
            {
                VUDKEditorUtility.DrawDisabledGroup(() =>
                {
                    int i = 0;
                    foreach (string save in files)
                        EditorGUILayout.LabelField(i++ + ". " + save);
                });
            });
        }
    }
}