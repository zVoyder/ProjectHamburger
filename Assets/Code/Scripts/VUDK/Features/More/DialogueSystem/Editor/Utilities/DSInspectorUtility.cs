namespace VUDK.Features.More.DialogueSystem.Editor.Utilities
{
    using System;
    using UnityEditor;

    public static class DSInspectorUtility
    {
        public static void DrawDisabledFields(Action action = null)
        {
            EditorGUI.BeginDisabledGroup(true);
            action?.Invoke();
            EditorGUI.EndDisabledGroup();
        }

        public static void DrawHeader(string label)
        {
            EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
        }

        public static void DrawPropertyField(this SerializedProperty serializedProperty)
        {
            EditorGUILayout.PropertyField(serializedProperty);
        }

        public static int DrawPopup(string label, SerializedProperty serializedIndexProperty, string[] options) 
        {
            return EditorGUILayout.Popup(label, serializedIndexProperty.intValue, options);
        }

        public static int DrawPopup(string label, int selectedIndex, string[] options)
        {
            return EditorGUILayout.Popup(label, selectedIndex, options);
        }

        public static void DrawSpace(int amount = 4)
        {
            EditorGUILayout.Space(amount);
        }

        public static void DrawHelpBox(string message, MessageType messageType = MessageType.Info, bool isWide = true)
        {
            EditorGUILayout.HelpBox(message, messageType, isWide);
        }
    }
}