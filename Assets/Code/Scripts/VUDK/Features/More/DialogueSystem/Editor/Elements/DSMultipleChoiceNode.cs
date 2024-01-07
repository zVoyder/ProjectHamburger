namespace VUDK.Features.More.DialogueSystem.Editor.Elements
{
    using UnityEditor.Experimental.GraphView;
    using UnityEngine;
    using UnityEngine.UIElements;
    using VUDK.Features.More.DialogueSystem.Data;
    using VUDK.Features.More.DialogueSystem.Editor.Data.Save;
    using VUDK.Features.More.DialogueSystem.Editor.Utilities;
    using VUDK.Features.More.DialogueSystem.Editor.Windows;
    using VUDK.Features.More.DialogueSystem.Enums;

    public class DSMultipleChoiceNode : DSNode
    {
        public override void Init(string nodeName, Vector2 position, DSGraphView graphView)
        {
            base.Init(nodeName, position, graphView);
            DialogueType = DSDialogueType.MultipleChoice;
            DSChoiceEditorData dsChoiceData = new DSChoiceEditorData()
            {
                Text = "New Choice",
            };
            Choices.Add(dsChoiceData);
        }

        public override void Draw()
        {
            base.Draw();

            #region MAIN CONTAINER

            Button addChoiceButton = DSElementUtility.CreateButton("Add Choice", () =>
            {
                DSChoiceEditorData dsChoiceData = new DSChoiceEditorData()
                {
                    Text = "New Choice",
                };
                Choices.Add(dsChoiceData);

                Port choicePort = CreateChoicePort(dsChoiceData);
                outputContainer.Add(choicePort);
            });

            addChoiceButton.AddToClassList("ds-node__button");

            mainContainer.Insert(1, addChoiceButton);

            #endregion MAIN CONTAINER

            #region OUTPUT CONTAINER

            foreach (DSChoiceEditorData choiceData in Choices)
            {
                Port choicePort = CreateChoicePort(choiceData);
                outputContainer.Add(choicePort);
            }

            #endregion OUTPUT CONTAINER

            RefreshExpandedState();
        }

        protected override void SetDialogueImage()
        {
            DialogueImage.image = DSIOUtility.LoadIcon("ico_nodeMultDialogue.png");
        }

        private Port CreateChoicePort(object userData)
        {
            Port choicePort = this.CreatePort();
            choicePort.userData = userData as DSChoiceEditorData;
            DSChoiceEditorData choiceData = choicePort.userData as DSChoiceEditorData;

            Button deleteChoiceButton = DSElementUtility.CreateButton("X", () =>
            {
                if (Choices.Count == 1) return;

                if (choicePort.connected)
                    GraphView.DeleteElements(choicePort.connections);

                Choices.Remove(choiceData);
                GraphView.RemoveElement(choicePort);
            });

            deleteChoiceButton.AddToClassList("ds-node__button");

            TextField choiceTextField = DSElementUtility.CreateTextField(choiceData.Text, null, callback =>
            {
                choiceData.Text = callback.newValue;
            });

            choiceTextField.AddClasses
            (
                "ds-node__text-field",
                "ds-node__choice-text-field",
                "ds-node__text-field__hidden"
            );

            choicePort.Add(choiceTextField);
            choicePort.Add(deleteChoiceButton);
            return choicePort;
        }
    }
}