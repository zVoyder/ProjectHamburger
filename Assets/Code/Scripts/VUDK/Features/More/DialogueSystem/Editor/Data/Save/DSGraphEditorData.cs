namespace VUDK.Features.More.DialogueSystem.Editor.Data.Save
{
    using System.Collections.Generic;
    using UnityEngine;
    using VUDK.Generic.Serializable;

    public class DSGraphEditorData : ScriptableObject
    {
        public string FileName;
        public List<DSGroupEditorData> Groups;
        public List<DSNodeEditorData> Nodes;
        public List<string> OldGroupNames;
        public List<string> OldUngroupedNodeNames;
        public SerializableDictionary<string, List<string>> OldGroupedNodeNames;

        public void Init(string fileName)
        {
            FileName = fileName;
            Groups = new List<DSGroupEditorData>();
            Nodes = new List<DSNodeEditorData>();
            OldGroupNames = new List<string>();
            OldUngroupedNodeNames = new List<string>();
            OldGroupedNodeNames = new SerializableDictionary<string, List<string>>();
        }
    }
}