namespace VUDK.Features.More.DialogueSystem.Components.Actors
{
    using UnityEngine;
    using VUDK.Features.More.DialogueSystem.Events;
    using VUDK.Generic.Serializable;

    public class DSActor : DSEmitter
    {
        [Header("Actor Animations")]
        [SerializeField]
        private Animator _animator;

        private DelayTask _clipDelayTask = new DelayTask();

        protected override void OnEnable()
        {
            base.OnEnable();
            _clipDelayTask.OnTaskCompleted += OnStop;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _clipDelayTask.OnTaskCompleted -= OnStop;
        }

        private void Update() => _clipDelayTask.Process();

        protected override void OnEmission(AudioClip clip)
        {
            base.OnEmission(clip);
            DSEvents.OnCompletedSentence -= OnStop;

            if(!IsMute)
                _clipDelayTask.Start(clip.length);
            else
                DSEvents.OnCompletedSentence += OnStop;

            _animator.SetBool("IsSpeaking", true);
        }

        protected override void OnStop()
        {
            base.OnStop();
            _clipDelayTask.Stop();
            _animator.SetBool("IsSpeaking", false);
        }
    }
}
