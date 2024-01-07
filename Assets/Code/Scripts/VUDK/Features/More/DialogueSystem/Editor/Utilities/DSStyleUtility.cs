namespace VUDK.Features.More.DialogueSystem.Editor.Utilities
{
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.UIElements;
    using VUDK.Features.More.DialogueSystem.Editor.Constants;

    public static class DSStyleUtility
    {
        public static VisualElement AddClasses(this VisualElement element, params string[] classNames)
        {
            foreach (string className in classNames)
                element.AddToClassList(className);

            return element;
        }

        public static VisualElement AddStyleSheets(this VisualElement element, params string[] styleSheetNames)
        {
            foreach (string styleSheetName in styleSheetNames)
            {
                try
                {
                    StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>($"{DSEditorPaths.StyleSheetsPath}/{styleSheetName}");
                    element.styleSheets.Add(styleSheet);
                }
                catch
                {
                    Debug.LogError($"Failed to load style sheet: {styleSheetName}, check if all the needed folders exist.");
                }
            }

            return element;
        }
    }
}