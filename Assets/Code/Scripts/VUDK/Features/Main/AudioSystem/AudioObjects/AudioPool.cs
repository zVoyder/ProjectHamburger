namespace VUDK.Features.Main.AudioSystem.AudioObjects
{
    using System.Collections.Generic;
    using UnityEngine;
    using VUDK.Features.Main.AudioSystem.AudioObjects.Interfaces;
    using VUDK.Generic.Serializable;

    public class AudioPool : MonoBehaviour, IAudioObject
    {
        [SerializeField]
        private List<AudioSource> _sources;

        private AudioSource _currentSource;
        private AudioClip _clip;

        private void OnValidate()
        {
            if (_sources == null) _sources = new List<AudioSource>();
        }

        /// <inheritdoc/>
        public void SetClip(AudioClip clip)
        {
            _clip = clip;
        }

        /// <summary>
        /// Plays the currently set audio clip using an available AudioSource.
        /// If no free AudioSource is found, a new one is created.
        /// </summary>
        public void Play()
        {
            if (TryFindFreeAudioSource(out AudioSource foundSource))
                _currentSource = foundSource;
            else
            {
                AddSource();
            }

            _currentSource.clip = _clip;
            _currentSource.Play();
        }

        /// <inheritdoc/>
        public void Stop()
        {
            _currentSource.Stop();
        }

        /// <summary>
        /// Sets the specified audio clip, plays it, and applies optional pitch variation.
        /// </summary>
        /// <param name="clip">The AudioClip to be played.</param>
        /// <param name="pitchVariation">Optional pitch variation range.</param>
        public void Play(AudioClip clip, Range<float> pitchVariation = null)
        {
            SetClip(clip);
            Play();

            if (pitchVariation != null)
                _currentSource.pitch = pitchVariation.Random();
        }

        /// <summary>
        /// Tries to find free audio source.
        /// </summary>
        /// <param name="audio">Found audio source.</param>
        /// <returns>True if found, false otherwise.</returns>
        private bool TryFindFreeAudioSource(out AudioSource audio)
        {
            foreach (AudioSource effect in _sources)
            {
                if (!effect.isPlaying)
                {
                    audio = effect;
                    return true;
                }
            }

            audio = null;
            return false;
        }

        public void AddSource()
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.playOnAwake = false;
            _sources.Add(source);
        }

#if UNITY_EDITOR
        public void Reset()
        {
            foreach (AudioSource source in GetComponents<AudioSource>())
                DestroyImmediate(source);
            _sources.Clear();
        }

        public void RemoveSource()
        {
            if(TryGetComponent(out AudioSource source))
            {
                _sources.Remove(source);
                DestroyImmediate(source);
            }
        }

        public static class PropertyNames
        {
            public static string SourcesProperty => nameof(_sources);
        }
#endif
    }
}