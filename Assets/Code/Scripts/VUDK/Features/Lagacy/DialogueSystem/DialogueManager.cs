namespace VUDK.Features.Legacy.DialogueSystem
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;
    using TMPro;
    using VUDK.Constants;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Features.Legacy.DialogueSystem.Data;
    using VUDK.Features.Main.InputSystem;
    using UnityEngine.InputSystem;

    [System.Obsolete]
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField, Min(0.01f), Header("Sentence")]
        private float _displayLetterTime = 0.1f;

        [SerializeField, Header("Dialogue")]
        private RectTransform _dialoguePanel;
        [SerializeField]
        private Image _speakerImage;
        [SerializeField]
        private TMP_Text _speakerName;
        [SerializeField]
        private TMP_Text _sentenceText;

        private Dialogue _dialogue;
        private Sentence _currentSentence;
        private SpeakerData _currentSpeaker;

        public bool IsDialogueOpen => _dialoguePanel.gameObject.activeSelf;
        public bool IsTalking { get; private set; }

        [Header("Events")]
        public UnityEvent OnBeginDialogue;
        public UnityEvent OnEndDialogue;

        private void Awake()
        {
            DisablePanel();
        }

        private void OnEnable()
        {
            EventManager.Ins.AddListener<Dialogue>(EventKeys.DialogueEvents.OnStartDialouge, StartDialogue);
            EventManager.Ins.AddListener(EventKeys.DialogueEvents.OnInterruptDialogue, InterruptDialogue);
            InputsManager.Inputs.Dialogue.SkipSentence.performed += SkipSentence;
        }

        private void OnDisable()
        {
            EventManager.Ins.RemoveListener<Dialogue>(EventKeys.DialogueEvents.OnStartDialouge, StartDialogue);
            EventManager.Ins.AddListener(EventKeys.DialogueEvents.OnInterruptDialogue, InterruptDialogue);
            InputsManager.Inputs.Dialogue.SkipSentence.performed -= SkipSentence;
        }

        public void StartDialogue(Dialogue dialogue)
        {
            Debug.Log("StartDialogue");
            EventManager.Ins.TriggerEvent(EventKeys.DialogueEvents.OnStartDialogue);
            OnBeginDialogue?.Invoke();
            _dialogue = dialogue;
            EnablePanel();
            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            if (_dialogue.IsEnded && !IsTalking)
            {
                EndDialogue();
                return;
            }

            StopAllCoroutines();
            if (!IsTalking)
            {
                _currentSentence = _dialogue.NextSentence();
                _currentSpeaker = _dialogue.GetSpeakerForSentence(_currentSentence);
                SetSentenceSpeaker(_currentSpeaker);
                StartCoroutine(TypeSentenceRoutine(_currentSentence));
            }
            else
            {
                IsTalking = false;
                SetCompleteSentence(_currentSentence);
            }
        }

        public void InterruptDialogue()
        {
            _dialogue.Reset();
            EndDialogue();
        }

        private void EndDialogue()
        {
            EventManager.Ins.TriggerEvent(EventKeys.DialogueEvents.OnEndDialogue);
            OnEndDialogue?.Invoke();
            DisablePanel();
            _sentenceText.text = "";
        }

        private void EnablePanel()
        {
            _dialoguePanel.gameObject.SetActive(true);
        }

        private void DisablePanel()
        {
            _dialoguePanel.gameObject.SetActive(false);
        }

        private void SetSentenceSpeaker(SpeakerData speaker)
        {
            _speakerImage.sprite = speaker.SpeakerImage;
            _speakerName.text = speaker.SpeakerName;
        }

        private void SetCompleteSentence(Sentence sentence)
        {
            SetSentenceSpeaker(_dialogue.GetSpeakerForSentence(sentence));
            _sentenceText.text = sentence.Phrase;
        }

        private void SkipSentence(InputAction.CallbackContext context)
        {
            if (!IsDialogueOpen) return;
            DisplayNextSentence();
        }

        private IEnumerator TypeSentenceRoutine(Sentence sentence)
        {
            _sentenceText.text = "";
            IsTalking = true;
            foreach (char letter in sentence.Phrase.ToCharArray())
            {
                _sentenceText.text += letter;
                yield return new WaitForSeconds(_displayLetterTime);
            }
            IsTalking = false;
        }
    }
}