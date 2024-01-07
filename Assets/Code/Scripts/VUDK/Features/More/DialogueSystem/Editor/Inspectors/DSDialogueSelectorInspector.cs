namespace VUDK.Features.More.DialogueSystem.Editor.Inspectors
{
    using System.Collections.Generic;
    using UnityEditor;
    using VUDK.Features.More.DialogueSystem.Data;
    using VUDK.Features.More.DialogueSystem.Editor.Utilities;
    using static VUDK.Features.More.DialogueSystem.DSDialogueSelectorBase;
    using static VUDK.Features.More.DialogueSystem.Editor.Constants.DSEditorPaths;
    using static VUDK.Features.More.DialogueSystem.Editor.Utilities.DSIOUtility;

    [CustomEditor(typeof(DSDialogueSelectorBase), true)]
    public class DSDialogueSelectorInspector : Editor
    {
        private SerializedProperty _dialogueContainerProperty;
        private SerializedProperty _dialogueGroupProperty;
        private SerializedProperty _dialogueProperty;
        private SerializedProperty _randomFirstDialogueProperty;
        private SerializedProperty _isInstantProperty;
        private SerializedProperty _isRepeatableProperty;
        private SerializedProperty _groupedDialoguesProperty;
        private SerializedProperty _startingDialoguesOnlyProperty;
        private SerializedProperty _selectedDialogueGroupIndexProperty;
        private SerializedProperty _selectedDialogueIndexProperty;

        private DSDialogueContainerData _dialogueContainer;

        private void OnEnable()
        {
            _dialogueContainerProperty = serializedObject.FindProperty(PropertyNames.DialogueContainerProperty);
            _dialogueProperty = serializedObject.FindProperty(PropertyNames.DialogueProperty);
            _randomFirstDialogueProperty = serializedObject.FindProperty(PropertyNames.RandomStartDialogueProperty);
            _dialogueGroupProperty = serializedObject.FindProperty(PropertyNames.DialogueGroupProperty);
            _isInstantProperty = serializedObject.FindProperty(PropertyNames.IsInstantDialogueProperty);
            _isRepeatableProperty = serializedObject.FindProperty(PropertyNames.IsRepeatableProperty);
            _groupedDialoguesProperty = serializedObject.FindProperty(PropertyNames.GroupedDialoguesProperty);
            _startingDialoguesOnlyProperty = serializedObject.FindProperty(PropertyNames.StartingDialoguesOnlyProperty);
            _selectedDialogueGroupIndexProperty = serializedObject.FindProperty(PropertyNames.SelectedDialogueGroupIndexProperty);
            _selectedDialogueIndexProperty = serializedObject.FindProperty(PropertyNames.SelectedDialogueIndexProperty);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawDialogueContainerArea();

            _dialogueContainer = _dialogueContainerProperty.objectReferenceValue as DSDialogueContainerData;

            if (_dialogueContainerProperty.objectReferenceValue == null)
            {
                StopDrawing("Please assign a Dialogue Container.");
                return;
            }

            DrawFiltersArea();

            bool currentStartingDialoguesOnlyFilter = _startingDialoguesOnlyProperty.boolValue;

            List<string> dialogueNames;
            string dialogueFolderPath = $"{DialoguesDataFolderPath}/{_dialogueContainer.FileName}";
            string dialogueInfoMessage;

            if(_groupedDialoguesProperty.boolValue)
            {
                List<string> dialogueGroupNames = _dialogueContainer.GetDialogueGroupNames();

                if (dialogueGroupNames.Count == 0)
                {
                    StopDrawing("There are no Dialogue Groups in the Dialogue Container.");
                    return;
                }

                DrawDialogueGroupArea(_dialogueContainer, dialogueGroupNames);

                DSDialogueGroupData dialogueGroup = _dialogueGroupProperty.objectReferenceValue as DSDialogueGroupData;
                dialogueNames = _dialogueContainer.GetGroupedDialogueNames(dialogueGroup, currentStartingDialoguesOnlyFilter);
                dialogueFolderPath += $"/Groups/{dialogueGroup.GroupName}/Dialogues";
                dialogueInfoMessage = $"There are no" + (currentStartingDialoguesOnlyFilter ? " Starting" : "") + "Dialogues in the Dialogue Group {dialogueGroup.GroupName}.";
            }
            else
            {
                dialogueNames = _dialogueContainer.GetUngroupedDialogueNames(currentStartingDialoguesOnlyFilter);
                dialogueFolderPath += "/Global/Dialogues";
                dialogueInfoMessage = "There are no" + (currentStartingDialoguesOnlyFilter ? " Starting" : "") + " Dialogues in the Dialogue Container.";
            }

            if(dialogueNames.Count == 0)
            {
                StopDrawing(dialogueInfoMessage);
                return;
            }

            DrawDialogueArea(dialogueNames, dialogueFolderPath);
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawDialogueContainerArea()
        {
            DSInspectorUtility.DrawHeader("Dialogue Container");
            _dialogueContainerProperty.DrawPropertyField();
            DSInspectorUtility.DrawSpace();
        }

        private void DrawFiltersArea()
        {
            DSInspectorUtility.DrawHeader("Filters");
            _groupedDialoguesProperty.DrawPropertyField();
            _startingDialoguesOnlyProperty.DrawPropertyField();
            DSInspectorUtility.DrawSpace();
        }

        private void DrawDialogueGroupArea(DSDialogueContainerData dialogueContainer, List<string> dialogueGroupNames)
        {
            DSInspectorUtility.DrawHeader("Dialogue Group");

            int oldSelectedDialogueGroupIndex = _selectedDialogueGroupIndexProperty.intValue;
            DSDialogueGroupData oldDialogueGroup = _dialogueGroupProperty.objectReferenceValue as DSDialogueGroupData;

            bool isOldDialogueGroupNull = oldDialogueGroup == null;
            string oldDialogueGroupName = isOldDialogueGroupNull ? string.Empty : oldDialogueGroup.GroupName;
            UpdateIndexOnNamesListUpdate(dialogueGroupNames, _selectedDialogueGroupIndexProperty, oldSelectedDialogueGroupIndex, oldDialogueGroupName, isOldDialogueGroupNull);

            _selectedDialogueGroupIndexProperty.intValue = DSInspectorUtility.DrawPopup("Dialogue Group", _selectedDialogueGroupIndexProperty, dialogueGroupNames.ToArray());

            string selectedDialogueGroupName = dialogueGroupNames[_selectedDialogueGroupIndexProperty.intValue];
            DSDialogueGroupData selectedDialogueGroup = LoadAsset<DSDialogueGroupData>($"{DialoguesDataFolderPath}/{dialogueContainer.FileName}/Groups/{selectedDialogueGroupName}", selectedDialogueGroupName);

            _dialogueGroupProperty.objectReferenceValue = selectedDialogueGroup;

            DSInspectorUtility.DrawDisabledFields(() =>
            {
                _dialogueGroupProperty.DrawPropertyField();
            });

            DSInspectorUtility.DrawSpace();
        }

        private void DrawDialogueArea(List<string> dialogueNames, string dialogueFolderPath)
        {
            DSInspectorUtility.DrawHeader("Dialogue Settings");
            _isRepeatableProperty.DrawPropertyField();
            _isInstantProperty.DrawPropertyField();
            _randomFirstDialogueProperty.DrawPropertyField();

            if (_randomFirstDialogueProperty.boolValue && _dialogueContainer.StartingDialogues.Count == 0 && _randomFirstDialogueProperty.boolValue)
            {
                StopDrawing("There are no Starting Dialogues in the Dialogue Container.", MessageType.Warning);
                return;
            }         

            EditorGUI.BeginDisabledGroup(_randomFirstDialogueProperty.boolValue);

            int oldSelectedDialogueIndex = _selectedDialogueIndexProperty.intValue;

            DSDialogueData oldDialogue = _dialogueProperty.objectReferenceValue as DSDialogueData;

            bool isOldDialogueNull = oldDialogue == null;
            string oldDialogueName = isOldDialogueNull ? string.Empty : oldDialogue.DialogueName;

            UpdateIndexOnNamesListUpdate(dialogueNames, _selectedDialogueIndexProperty, oldSelectedDialogueIndex, oldDialogueName, isOldDialogueNull);

            _selectedDialogueIndexProperty.intValue = DSInspectorUtility.DrawPopup("Select Dialogue", _selectedDialogueIndexProperty, dialogueNames.ToArray());

            string selectedDialogueName = dialogueNames[_selectedDialogueIndexProperty.intValue];

            DSDialogueData selectedDialogue = LoadAsset<DSDialogueData>(dialogueFolderPath, selectedDialogueName);
            _dialogueProperty.objectReferenceValue = selectedDialogue;

            DSInspectorUtility.DrawDisabledFields(() =>
            {
                _dialogueProperty.DrawPropertyField();
            });

            EditorGUI.EndDisabledGroup();
            DSInspectorUtility.DrawSpace();
        }

        private void StopDrawing(string reason, MessageType messageType = MessageType.Info)
        {
            DSInspectorUtility.DrawHelpBox(reason, messageType);
            DSInspectorUtility.DrawSpace();
            DSInspectorUtility.DrawHelpBox("You need to seleect a Dialogue for this component to work properly at Runtime!", MessageType.Warning);
            serializedObject.ApplyModifiedProperties();
        }

        private void UpdateIndexOnNamesListUpdate(List<string> optionNames, SerializedProperty indexProperty ,int oldSelectedPropertyIndex, string oldPropertyName, bool isOldPropertyNull)
        {
            if (isOldPropertyNull)
            {
                indexProperty.intValue = 0;
                return;
            }

            bool oldIndexIsOutOfBoundsOfNamesListCount = oldSelectedPropertyIndex > optionNames.Count - 1;
            bool oldNameIsDifferentThanSelectedName = oldIndexIsOutOfBoundsOfNamesListCount || oldPropertyName != optionNames[oldSelectedPropertyIndex];

            if (oldNameIsDifferentThanSelectedName)
            {
                if (optionNames.Contains(oldPropertyName))
                {
                    indexProperty.intValue = optionNames.IndexOf(oldPropertyName);
                }
                else
                {
                    indexProperty.intValue = 0;
                }
            }
        }
    }
}
