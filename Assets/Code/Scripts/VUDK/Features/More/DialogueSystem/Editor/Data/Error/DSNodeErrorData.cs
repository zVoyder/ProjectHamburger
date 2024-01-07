namespace VUDK.Features.More.DialogueSystem.Editor.Data.Error
{
    using System.Collections.Generic;
    using VUDK.Features.More.DialogueSystem.Editor.Elements;

    public class DSNodeErrorData
    {
        public DSErrorData ErrorData { get; private set; }
        public List<DSNode> Nodes { get; private set; }

        public DSNodeErrorData()
        {
            ErrorData = new DSErrorData();
            Nodes = new List<DSNode>();
        }
    }
}