namespace VUDK.Features.Legacy.DialogueSystem
{
    using System.Collections.Generic;
    using UnityEngine;
    using VUDK.Features.Legacy.DialogueSystem.Data;

    [System.Serializable]
    [System.Obsolete]
    public class Dialogue
    {
        [SerializeField, Header("Speakers")]
        private List<SpeakerData> _speakers;

        [SerializeField, Header("Sentences")]
        private List<Sentence> _sentences;

        private int _index = 0;

        public bool IsEnded => _index == _sentences.Count;

        public Sentence NextSentence()
        {
            return _sentences[_index++];
        }

        public Sentence PreviousSentence()
        {
            return _sentences[_index--];
        }

        public Sentence CurrentSentence()
        {
            return _sentences[_index];
        }

        public SpeakerData GetSpeakerForSentence(Sentence sentence)
        {
            return _speakers[sentence.SpeakerId];
        }

        public void Reset()
        {
            _index = 0;
        }
    }
}