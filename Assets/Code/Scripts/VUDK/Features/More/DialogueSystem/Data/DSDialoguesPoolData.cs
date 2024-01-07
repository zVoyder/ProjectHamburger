namespace VUDK.Features.More.DialogueSystem.Data
{
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = nameof(DSDialoguesPoolData), menuName = "VUDK/Dialogue System/Dialogues Pool")]
    public class DSDialoguesPoolData : ScriptableObject
    {
        public List<DSDialogueContainerData> Pool;
    }
}