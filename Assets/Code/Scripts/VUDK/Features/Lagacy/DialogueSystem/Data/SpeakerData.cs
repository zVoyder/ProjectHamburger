namespace VUDK.Features.Legacy.DialogueSystem.Data
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "VUDK/Legacy/DialogueSpeaker")]
    [System.Obsolete]
    public class SpeakerData : ScriptableObject
    {
        public string SpeakerName;
        public Sprite SpeakerImage;
    }
}