namespace VUDK.Editor.Tools.Utility
{
    using UnityEngine.InputSystem;
    using UnityEditor;
    using UnityEngine;

    public class InputsWindow : EditorWindow
    {
        private static InputActionAsset s_inputActionAsset;

        [MenuItem("VUDK/Utility/Inputs")]
        public static void OpenInputsMap()
        {
            if(!s_inputActionAsset)
                FindInputActionAsset();

            if(s_inputActionAsset)
                AssetDatabase.OpenAsset(s_inputActionAsset);
        }

        private static void FindInputActionAsset()
        {
            string[] guids = AssetDatabase.FindAssets("Inputs t:InputActionAsset");
            if (guids.Length > 0)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                s_inputActionAsset = AssetDatabase.LoadAssetAtPath<InputActionAsset>(path);
            }

            if (!s_inputActionAsset)
                Debug.LogWarning("Asset \"Inputs.inputActionAsset\" not found.");
        }
    }
}
