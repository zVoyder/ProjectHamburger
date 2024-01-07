namespace VUDK.Features.Legacy.DialogueSystem
{
    using UnityEngine;
    using VUDK.Constants;
    using VUDK.Features.Main.EventSystem;

    [System.Obsolete]
    public class DialogueTrigger : MonoBehaviour
    {
        [Header("Dialogue")]
        [SerializeField]
        private Dialogue _dialogue;

        public void Trigger()
        {
            EventManager.Ins.TriggerEvent(EventKeys.DialogueEvents.OnStartDialouge, _dialogue);
        }

        public void Interrupt()
        {
            EventManager.Ins.TriggerEvent(EventKeys.DialogueEvents.OnInterruptDialogue);
        }
    }
}
