namespace VUDK.Features.Main.AudioSystem
{
    using UnityEngine;
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

        private AudioPool _stereoPool;

        [field: SerializeField, Header("Pooling Settings")]
        public ScriptableKey SFXPoolKey { get; private set;}

        private void Awake()
        {
            TryGetComponent(out _stereoPool);
        }

        /// <summary>
        /// Plays the specified audio clip at a specified 3D position in the scene.
        /// </summary>
        /// <param name="clip">The AudioClip to be played spatially.</param>
        /// <param name="position">The 3D position in the scene where the audio should be played.</param>
        public void PlaySpatial(AudioClip clip, Vector3 position)
        {
            AudioExtension.PlayClipAtPoint(clip, position);
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

        /// <summary>
        /// Plays the specified audio clip source from the audio stereo pool.
        /// </summary>
        /// <param name="clip">The AudioClip to be played from the audio pool.</param>
        /// <param name="pitchVariation">Optional pitch variation range.</param>
        public void PlayPool(AudioClip clip, Range<float> pitchVariation = null)
        {
            _stereoPool.Play(clip, pitchVariation);
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
    }
}