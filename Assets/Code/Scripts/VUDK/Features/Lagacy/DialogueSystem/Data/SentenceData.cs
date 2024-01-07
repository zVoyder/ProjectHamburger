namespace VUDK.Features.Legacy.DialogueSystem.Data
{
    using UnityEngine;

    [System.Serializable]
    [System.Obsolete]
    public struct Sentence
    {
        public int SpeakerId;
        [TextArea(3, 10)]
        public string Phrase;
    }
}