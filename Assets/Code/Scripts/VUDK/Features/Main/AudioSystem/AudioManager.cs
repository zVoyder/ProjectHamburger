namespace VUDK.Features.Main.AudioSystem
{
    using UnityEngine;
    using UnityEngine.Audio;
    using VUDK.Extensions;
    using VUDK.Features.Main.AudioSystem.AudioObjects;
    using VUDK.Features.Main.ScriptableKeys;
    using VUDK.Generic.Serializable;

    [DefaultExecutionOrder(-900)]
    [RequireComponent(typeof(AudioPool))]
    public sealed class AudioManager : MonoBehaviour
    {
        [SerializeField, Header("Main Sources")]
        private AudioSource _mainMusic;
        [field: SerializeField]
        private AudioSource _mainEffect;

        [SerializeField, Header("Mixer Groups")]
        private AudioMixerGroup _effectMixerGroup;

        private AudioPool _stereoPool;

        [field: SerializeField, Header("Pooling Settings")]
        public ScriptableKey SFXPoolKey { get; private set;}

        private void Awake()
        {
            TryGetComponent(out _stereoPool);
        }

        public void ToggleAudioListener()
        {
            SetPauseAudioListener(!AudioListener.pause);
        }

        public void SetPauseAudioListener(bool isPaused)
        {
            AudioListener.pause = isPaused;
        }

        public void EanbleAudio()
        {
            SetPauseAudioListener(true);
        }

        public void DisableAudio()
        {
            SetPauseAudioListener(false);
        }

        /// <summary>
        /// Plays the specified audio clip at a specified 3D position in the scene.
        /// </summary>
        /// <param name="clip">The AudioClip to be played spatially.</param>
        /// <param name="position">The 3D position in the scene where the audio should be played.</param>
        public AudioSFX PlaySpatial(AudioClip clip, Vector3 position)
        {
            return AudioExtension.PlayClipAtPoint(clip, position);
        }

        /// <summary>
        /// Plays the specified audio clip either as a concurrent stereo effect or the main effect.
        /// </summary>
        /// <param name="clip">The AudioClip to be played in stereo.</param>
        /// <param name="isConcurrent">If true, plays the audio concurrently using the audio pool; otherwise, plays it as the main effect.</param>
        public void PlayStereo(AudioClip clip, bool isConcurrent = false)
        {
            if(isConcurrent)
                PlayPool(clip);
            else
                PlayMain(clip);
        }

        public void PlayStereo(AudioSourceSettings sourceSettings, bool isConcurrent = false)
        {
            if (isConcurrent)
                PlayPool(sourceSettings);
            else
                PlayMain(sourceSettings);
        }

        /// <summary>
        /// Plays the specified audio clip source from the audio stereo pool.
        /// </summary>
        /// <param name="clip">The AudioClip to be played from the audio pool.</param>
        /// <param name="pitchVariation">Optional pitch variation range.</param>
        public void PlayPool(AudioClip clip, Range<float> pitchVariation = null)
        {
            AudioSource ac = _stereoPool.Play(clip, pitchVariation);
            ac.outputAudioMixerGroup = _effectMixerGroup;
        }

        public void PlayPool(AudioSourceSettings sourceSettings)
        {
            AudioSource ac = _stereoPool.Play(sourceSettings);
            ac.outputAudioMixerGroup = _effectMixerGroup;
        }

        /// <summary>
        /// Plays the specified audio clip as a main effect.
        /// </summary>
        /// <param name="clip">The AudioClip to be played as the main effect.</param>
        /// <param name="pitchVariation">Optional pitch variation range.</param>
        public void PlayMain(AudioClip clip, Range<float> pitchVariation = null)
        {
            PlayAudio(_mainEffect, clip, pitchVariation);
        }

        public void PlayMain(AudioSourceSettings sourceSettings, Range<float> pitchVariation = null)
        {
            PlayAudio(_mainEffect, sourceSettings, pitchVariation);
        }

        /// <summary>
        /// Plays the specified audio clip as the main music.
        /// </summary>
        /// <param name="clip">The AudioClip to be played as the main music.</param>
        public void PlayMusic(AudioClip clip)
        {
            _mainMusic.clip = clip;
            _mainMusic.Play();
        }

        /// <summary>
        /// Plays an audio clip.
        /// </summary>
        /// <param name="source">Audio source.</param>
        /// <param name="clip">Audio clip.</param>
        /// <param name="pitchVariation">Pitch variation.</param>
        /// <returns>True if played, false otherwise.</returns>
        private void PlayAudio(AudioSource source, AudioClip clip, Range<float> pitchVariation = null)
        {
            if(pitchVariation != null)
                source.pitch = pitchVariation.Random();

            source.clip = clip;
            source.Play();
        }

        private void PlayAudio(AudioSource source, AudioSourceSettings sourceSettings, Range<float> pitchVariation = null)
        {
            source.SetAudioSourceSettings(sourceSettings);

            if (pitchVariation != null)
                source.pitch = pitchVariation.Random();

            source.Play();
        }
    }
}