namespace VUDK.Features.More.DialogueSystem.Editor.Windows
{
    using UnityEditor;
    using UnityEngine;
    using VUDK.Features.More.DialogueSystem.Editor.Utilities;
    using VUDK.Extensions;

    public class DSActorWindow : EditorWindow
    {
        private Sprite _actorIcon;
        private string _actorName;

        public static void Open()
        {
            DSActorWindow window = GetWindow<DSActorWindow>();
            window.titleContent = new GUIContent("Actor Creator");
        }

        private void OnGUI()
        {
            GUILayout.Label("Dialogue Actor", EditorStyles.boldLabel);
            _actorIcon = (Sprite)EditorGUILayout.ObjectField("Actor Icon", _actorIcon, typeof(Sprite), false);
            _actorName = EditorGUILayout.TextField("Actor Name", _actorName);
            CheckCreate();
        }

        private void CheckCreate()
        {
            bool isActorNameEmpty = string.IsNullOrEmpty(_actorName) || _actorName.RemoveWhitespaces() == "";
            GUI.enabled = !isActorNameEmpty;

            if (GUILayout.Button("Create Actor"))
                DSIOUtility.CreateActorAsset(_actorIcon, _actorName);

            GUI.enabled = true;
        }
    }
}
