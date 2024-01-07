namespace VUDK.Features.More.DialogueSystem.Components.Actors
{
    using UnityEngine;
    using UnityEngine.Events;
    using VUDK.Features.More.DialogueSystem.Data;
    using VUDK.Features.More.DialogueSystem.Events;

    [DisallowMultipleComponent]
    [RequireComponent(typeof(AudioSource))]
    public class DSEmitter : MonoBehaviour
    {
        [Header("Actor Data")]
        [SerializeField]
        protected DSActorData ActorData;

        protected AudioSource AudioSource;

        protected bool IsMute { get; private set; }

        [Header("Events")]
        public UnityEvent OnBeginSpeaking;
        public UnityEvent OnEndSpeaking;

        protected virtual void Awake()
        {
            TryGetComponent(out AudioSource audioSource);
            AudioSource = audioSource;
        }

        protected virtual void OnEnable()
        {
            ActorData.OnEmitDialogue += OnEmission;
            DSEvents.OnNext += OnStop;
            DSEvents.OnDisable += OnStop;
        }

        protected virtual void OnDisable()
        {
            ActorData.OnEmitDialogue -= OnEmission;
            DSEvents.OnNext -= OnStop;
            DSEvents.OnDisable -= OnStop;
        }

        protected virtual void OnEmission(AudioClip clip)
        {
            OnBeginSpeaking?.Invoke();
            IsMute = clip == null;
            
            if(!IsMute) AudioSource.PlayOneShot(clip);
        }

        protected virtual void OnStop()
        {
            OnEndSpeaking?.Invoke();
            if (!IsMute) AudioSource.Stop();
        }
    }
}