namespace VUDK.Features.Packages.ExplorationSystem.Editor
{
    using UnityEditor;
    using UnityEngine;
    using VUDK.Features.More.ExplorationSystem.Transition;
    using VUDK.Features.More.ExplorationSystem.Transition.Types.Keys;

    [CustomEditor(typeof(TransitionMachine), true)]
    public class TransitionMachineEditor : Editor
    {
        private TransitionMachine _target;
        private SerializedProperty _transitionTypeProperty;
        private SerializedProperty _timeProcessProperty;
        private SerializedProperty _fovChangerProperty;
        private TransitionType _previousTransitionType;

        private void OnEnable()
        {
            _target = target as TransitionMachine;
            _transitionTypeProperty = serializedObject.FindProperty("_transitionType");
            _timeProcessProperty = serializedObject.FindProperty("_timeProcess");
            _fovChangerProperty = serializedObject.FindProperty("_fovChanger");
            _previousTransitionType = (TransitionType)_transitionTypeProperty.enumValueIndex;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_transitionTypeProperty);

            TransitionType currentTransitionType = (TransitionType)_transitionTypeProperty.enumValueIndex;

            if (_previousTransitionType != currentTransitionType)
                ChangeTransitionType(currentTransitionType);

            ShowCorrectFields(currentTransitionType);
            serializedObject.ApplyModifiedProperties();
        }

        private void ChangeTransitionType(TransitionType transitionType)
        {
            if (Application.isPlaying)
                _target.ChangeDefaultTransition(transitionType);
            _previousTransitionType = transitionType;
        }

        private void ShowCorrectFields(TransitionType transitionType)
        {
            switch (transitionType)
            {
                case TransitionType.Fov:
                    EditorGUILayout.PropertyField(_timeProcessProperty);
                    EditorGUILayout.PropertyField(_fovChangerProperty);
                    break;

                case TransitionType.Linear:
                    EditorGUILayout.PropertyField(_timeProcessProperty);
                    break;

                default:
                    break;
            }
        }
    }

}