namespace VUDK.Features.Main.AudioSystem.AudioObjects
{
    using UnityEngine;
    using VUDK.Patterns.Pooling;
    using VUDK.Extensions;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Serializable;
    using VUDK.Features.Main.AudioSystem.AudioObjects.Interfaces;
    using VUDK.Features.Main.ScriptableKeys;

    [RequireComponent(typeof(AudioSource))]
    public class AudioSFX : PooledObject, IAudioObject
    {
        private AudioSource _audioSource;
        private DelayTask _clipLenghtTask;

        private void Awake()
        {
            TryGetComponent(out _audioSource);
            _clipLenghtTask = new DelayTask();
        }

        private void OnEnable()
        {
            _clipLenghtTask.OnTaskCompleted += Stop;
        }

        private void OnDisable()
        {
            _clipLenghtTask.OnTaskCompleted -= Stop;
        }

        private void Update() => _clipLenghtTask.Process();

        public static AudioSFX Create(AudioClip clip)
        {
            ScriptableKey poolKey = MainManager.Ins.AudioManager.SFXPoolKey;
            GameObject goAud = MainManager.Ins.PoolsManager.Pools[poolKey].Get();
            if (goAud.TryGetComponent(out AudioSFX audioSFX))
                audioSFX.SetClip(clip);

            return audioSFX;
        }

        /// <inheritdoc/>
        public void SetClip(AudioClip clip)
        {
            _audioSource.clip = clip;
        }

        /// <inheritdoc/>
        public void Play()
        {
            PlayAtPosition(transform.position);
        }

        /// <summary>
        /// Plays the audio clip at the specified position.
        /// </summary>
        /// <param name="position">The position in the scene where the audio should be played.</param>
        public void PlayAtPosition(Vector3 position)
        {
            _audioSource.Play();
            _clipLenghtTask.Start(_audioSource.clip.length);
            transform.SetPosition(position);
        }

        /// <inheritdoc/>
        public void Stop()
        {
            Dispose();
        }

        /// <inheritdoc/>
        public override void Clear()
        {
            _audioSource.Stop();
            _audioSource.clip = null;
        }
    }
}