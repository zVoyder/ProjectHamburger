namespace VUDK.Features.More.DialogueSystem.Editor.Data.Error
{
    using UnityEngine;

    public class DSErrorData
    {
        public Color Color;

        public DSErrorData()
        {
            GenerateRandomColor();
        }

        private void GenerateRandomColor()
        {
            Color = new Color32(
                (byte)Random.Range(65, 256),
                (byte)Random.Range(50, 176),
                (byte)Random.Range(50, 176),
                255);
        }
    }
}