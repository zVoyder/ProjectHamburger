namespace VUDK.Features.More.DialogueSystem.Editor.Utilities
{
    using System;
    using UnityEditor.Experimental.GraphView;
    using UnityEditor.UIElements;
    using UnityEngine.UIElements;
    using VUDK.Features.More.DialogueSystem.Editor.Elements;

    public static class DSElementUtility
    {
        public static TextField CreateTextField(string value = null, string label = null, EventCallback<ChangeEvent<string>> onValueChanged = null)
        {
            TextField textField = new TextField()
            {
                value = value,
                label = label
            };

            if (onValueChanged != null)
                textField.RegisterValueChangedCallback(onValueChanged);

            return textField;
        }

        public static TextField CreateTextArea(string value = null, string label = null, EventCallback<ChangeEvent<string>> onValueChanged = null)
        {
            TextField textArea = CreateTextField(value, label, onValueChanged);
            textArea.multiline = true;

            return textArea;
        }

        public static Foldout CreateFoldout(string title, bool collapsed = false)
        {
            Foldout foldout = new Foldout()
            {
                text = title,
                value = !collapsed
            };

            return foldout;
        }

        public static ObjectField CreateObjectField<T>(UnityEngine.Object value = null, string label = null, EventCallback<ChangeEvent<UnityEngine.Object>> onValueChanged = null) where T : UnityEngine.Object
        {
            ObjectField objectField = new ObjectField()
            {
                objectType = typeof(T),
                value = value,
                label = label
            };

            if (onValueChanged != null)
                objectField.RegisterValueChangedCallback(onValueChanged);

            return objectField;
        }

        public static Button CreateButton(string text, Action onClick = null)
        {
            Button button = new Button(onClick)
            {
                text = text
            };

            return button;
        }

        public static Port CreatePort(this DSNode node, string portName = "", Orientation orientation = Orientation.Horizontal, Direction direction = Direction.Output, Port.Capacity capacity = Port.Capacity.Single)
        {
            Port port = node.InstantiatePort(orientation, direction, capacity, typeof(bool));

            port.portName = portName;

            return port;
        }
    }
}