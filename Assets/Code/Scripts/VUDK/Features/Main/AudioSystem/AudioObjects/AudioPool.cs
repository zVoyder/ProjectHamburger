namespace VUDK.Features.Main.AudioSystem.AudioObjects
{
    using System.Collections.Generic;
    using UnityEngine;
    using VUDK.Extensions;
    using VUDK.Generic.Serializable;

    public class AudioPool : MonoBehaviour
    {
        [SerializeField]
        private List<AudioSource> _sources;

        private void OnValidate()
        {
            if (_sources == null)
                _sources = new List<AudioSource>();
        }

        /// <summary>
        /// Plays the currently set audio clip using an available AudioSource.
        /// If no free AudioSource is found, a new one is created.
        /// </summary>
        public AudioSource Play(AudioClip clip, Range<float> pitchVariation = null)
        {
            AudioSource source = GetAvailableSource();

            if (pitchVariation != null)
                source.pitch = pitchVariation.Random();

            source.clip = clip;
            source.Play();

            return source;
        }

        public AudioSource Play(AudioSourceSettings settings)
        {
            AudioSource source = GetAvailableSource();
            source.SetAudioSourceSettings(settings);
            source.Play();

            return source;
        }

        /// <summary>
        /// Stops all playing audio sources.
        /// </summary>
        public void Stop()
        {
            foreach (AudioSource source in _sources)
                source.Stop();
        }

        public AudioSource AddSource()
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.playOnAwake = false;
            _sources.Add(source);
            return source;
        }

        private AudioSource GetAvailableSource()
        {
            if (TryFindFreeAudioSource(out AudioSource audio))
                return audio;

            return AddSource();
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

#if UNITY_EDITOR

        public void Reset()
        {
            foreach (AudioSource source in GetComponents<AudioSource>())
                DestroyImmediate(source);
            _sources.Clear();
        }

        public void RemoveSource()
        {
            if (TryGetComponent(out AudioSource source))
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