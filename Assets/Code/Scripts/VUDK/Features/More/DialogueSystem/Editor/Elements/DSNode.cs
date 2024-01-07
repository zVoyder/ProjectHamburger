namespace VUDK.Features.More.DialogueSystem.Editor.Elements
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEditor.Experimental.GraphView;
    using UnityEditor.UIElements;
    using UnityEngine;
    using UnityEngine.UIElements;
    using VUDK.Extensions;
    using VUDK.Features.More.DialogueSystem.Data;
    using VUDK.Features.More.DialogueSystem.Editor.Data.Save;
    using VUDK.Features.More.DialogueSystem.Editor.Utilities;
    using VUDK.Features.More.DialogueSystem.Editor.Windows;
    using VUDK.Features.More.DialogueSystem.Enums;

    public class DSNode : Node
    {
        public string NodeID;
        public string DialogueName;
        public string DialogueText;
        public DSActorData ActorData;
        public AudioClip DialogueAudioClip;
        public List<DSChoiceEditorData> Choices;
        public DSDialogueType DialogueType;
        public DSGroup Group;

        protected DSGraphView GraphView;
        protected Image DialogueImage;
        private Color _defaultBackgroundColor;

        public virtual void Init(string nodeName, Vector2 position, DSGraphView graphView)
        {
            NodeID = Guid.NewGuid().ToString();
            Choices = new List<DSChoiceEditorData>();
            DialogueImage = new Image();
            GraphView = graphView;
            DialogueName = nodeName;
            DialogueText = "Dialogue text.";
            _defaultBackgroundColor = new Color(29f / 255f, 29f / 255f, 30f / 255f);
            SetPosition(new Rect(position, Vector2.zero));
            mainContainer.AddToClassList("ds-node__main-container");
            extensionContainer.AddToClassList("ds-node__extension-container");
            SetDialogueImage();
        }

        public virtual void Draw()
        {
            #region CONTENT CONTAINER

            contentContainer.Insert(0, DialogueImage);

            #endregion CONTENT CONTAINER

            #region TITLE CONTAINER

            TextField dialogueNameTextField = DSElementUtility.CreateTextField(DialogueName, null, callback =>
            {
                TextField target = (TextField)callback.target;
                target.value = callback.newValue.RemoveSpecialAndWhitespaces();

                if (string.IsNullOrEmpty(target.value))
                {
                    if (!string.IsNullOrEmpty(DialogueName))
                    {
                        ++GraphView.NameErrorsAmount;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(DialogueName))
                    {
                        --GraphView.NameErrorsAmount;
                    }
                }

                if (Group == null)
                {
                    GraphView.RemoveUngroupedNode(this);
                    DialogueName = target.value;
                    GraphView.AddUngroupedNode(this);
                    return;
                }

                DSGroup currentGroup = Group;

                GraphView.RemoveNodeFromGroup(this, Group);

                DialogueName = target.value;

                GraphView.AddNodeInGroup(this, currentGroup);
            });

            dialogueNameTextField.AddClasses
            (
                "ds-node__text-field",
                "ds-node__filename-text-field",
                "ds-node__text-field__hidden"
            );

            titleContainer.Insert(0, dialogueNameTextField);

            #endregion TITLE CONTAINER

            #region INPUT CONTAINER

            Port inputPort = DSElementUtility.CreatePort(this, "Dialogue Input", Orientation.Horizontal, Direction.Input, Port.Capacity.Multi);
            inputContainer.Add(inputPort);

            #endregion INPUT CONTAINER

            #region EXTENSIONS CONTAINER

            VisualElement customDataContainer = new VisualElement();

            customDataContainer.AddToClassList("ds-node__custom-data-container");

            Foldout textFoldout = DSElementUtility.CreateFoldout("Dialogue Text", true);
            TextField textTextField = DSElementUtility.CreateTextArea(DialogueText, null, callback =>
            {
                DialogueText = callback.newValue;
            });

            textTextField.AddClasses
            (
                "ds-node__text-field",
                "ds-node__text-field__hidden"
            );

            ObjectField actorObject = DSElementUtility.CreateObjectField<DSActorData>(ActorData, "Actor", callback =>
            {
                ActorData = callback.newValue as DSActorData;
            });

            ObjectField audioClipObject = DSElementUtility.CreateObjectField<AudioClip>(DialogueAudioClip, "Audio Clip", callback =>
            {
                DialogueAudioClip = callback.newValue as AudioClip;
            });

            textFoldout.Add(textTextField);
            customDataContainer.Add(actorObject);
            customDataContainer.Add(audioClipObject);
            customDataContainer.Add(textFoldout);
            extensionContainer.Add(customDataContainer);
            
            #endregion EXTENSIONS CONTAINER
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            evt.menu.AppendAction("Disconnect Input Ports", action =>
            {
                DisconnectInputPorts();
            });

            evt.menu.AppendAction("Disconnect Output Ports", action =>
            {
                DisconnectOutputPorts();
            });

            base.BuildContextualMenu(evt);
        }

        public bool IsStartNode()
        {
            Port inputPort = inputContainer.Children().First() as Port;

            return !inputPort.connected;
        }

        public void SetErrorStyle(Color color)
        {
            mainContainer.style.backgroundColor = color;
        }

        public void ResetStyle()
        {
            mainContainer.style.backgroundColor = _defaultBackgroundColor;
        }

        public void DisconnectAllPorts()
        {
            DisconnectInputPorts();
            DisconnectOutputPorts();
        }

        protected virtual void SetDialogueImage()
        {
            DialogueImage.image = DSIOUtility.LoadIcon("ico_nodeDialogue.png");
        }

        private void DisconnectInputPorts()
        {
            DisconnectPorts(inputContainer);
        }

        private void DisconnectOutputPorts()
        {
            DisconnectPorts(outputContainer);
        }

        private void DisconnectPorts(VisualElement container)
        {
            foreach (Port port in container.Children())
            {
                if (!port.connected)
                    continue;

                GraphView.DeleteElements(port.connections);
            }
        }
    }
}