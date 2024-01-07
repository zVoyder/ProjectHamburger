namespace VUDK.Features.More.DialogueSystem.Editor.Elements
{
    using UnityEngine;
    using UnityEditor.Experimental.GraphView;
    using VUDK.Features.More.DialogueSystem.Editor.Utilities;
    using VUDK.Features.More.DialogueSystem.Enums;
    using VUDK.Features.More.DialogueSystem.Editor.Windows;
    using VUDK.Features.More.DialogueSystem.Editor.Data.Save;

    public class DSSingleChoiceNode : DSNode
    {
        public override void Init(string nodeName, Vector2 position, DSGraphView graphView)
        {
            base.Init(nodeName, position, graphView);
            DialogueType = DSDialogueType.SingleChoice;

            DSChoiceEditorData dSChoiceSaveData = new DSChoiceEditorData()
            {
                Text = "Next Dialogue",
            };

            Choices.Add(dSChoiceSaveData);
        }

        public override void Draw()
        {
            base.Draw();

            #region OUTPUT CONTAINER

            foreach (DSChoiceEditorData choiceData in Choices)
            {
                Port choicePort = this.CreatePort(choiceData.Text);
                choicePort.userData = choiceData;
                outputContainer.Add(choicePort);
            }

            #endregion OUTPUT CONTAINER

            RefreshExpandedState();
        }
    }
}