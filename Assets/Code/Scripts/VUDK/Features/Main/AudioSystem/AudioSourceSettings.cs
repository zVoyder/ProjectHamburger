namespace VUDK.Features.Main.AudioSystem
{
    using UnityEngine;
    using UnityEngine.Audio;

    [System.Serializable]
    public class AudioSourceSettings
    {
        public AudioClip Clip;
        public bool Loop = false;
        public bool PlayOnAwake = false;
        [Range(0f, 1f)]
        public float Volume = 1f;
        [Range(-3f, 3f)]
        public float MinPitch = 1f;
        [Range(-3f, 3f)]
        public float MaxPitch = 1f;
        [Range(-1f, 1f)]
        public float PanStereo = 0f;
        [Range(0f, 1f)]
        public float SpatialBlend = 0f;
        [Range(0f, 1.1f)]
        public float ReverbZoneMix = 1f;
        public AudioRolloffMode RolloffMode = AudioRolloffMode.Logarithmic;
        [Min(0f)]
        public float MinDistance = 1f;
        [Min(0f)]
        public float MaxDistance = 500f;
        public AudioMixerGroup OutputAudioMixerGroup;
    }
}