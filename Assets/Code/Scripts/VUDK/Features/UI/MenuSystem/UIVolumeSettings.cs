namespace VUDK.Features.UI.MenuSystem
{
    using UnityEngine;
    using UnityEngine.Audio;
    using UnityEngine.UI;
    using VUDK.Features.UI.MenuSystem.MenuPreferences;

    public class UIVolumeSettings : MonoBehaviour
    {
        [SerializeField, Header("Mixer Group")]
        private AudioMixer _mixer;

        [SerializeField, Header("Sliders")]
        private Slider _masterSlider;
        [SerializeField]
        private Slider _musicSlider;
        [SerializeField]
        private Slider _effectsSlider;

        private void Start() // Need to do it on start cause the mixer is loaded after the awake
        {
            if (MenuPrefsSaver.Audio.LoadVolume(out float master, out float music, out float sfx))
            {
                _mixer.SetFloat("Master", master);
                _mixer.SetFloat("Music", music);
                _mixer.SetFloat("Effects", sfx);

                _masterSlider.value = master;
                _musicSlider.value = music;
                _effectsSlider.value = sfx;
            }
            else
            {
                // If there were no saved values then we set the volume to 0 (that means +0dB)
                _mixer.SetFloat("Master", 0f);
                _mixer.SetFloat("Music", 0f);
                _mixer.SetFloat("Effects", 0f);
            }
        }

        /// <summary>
        /// Sets the master volume.
        /// </summary>
        public void SetMaster()
        {
            _mixer.SetFloat("Master", _masterSlider.value);
            MenuPrefsSaver.Audio.SaveVolume(_masterSlider.value, _musicSlider.value, _effectsSlider.value);
        }

        /// <summary>
        /// Sets the music volume.
        /// </summary>
        public void SetMusic()
        {
            _mixer.SetFloat("Music", _musicSlider.value);
            MenuPrefsSaver.Audio.SaveVolume(_masterSlider.value, _musicSlider.value, _effectsSlider.value);
        }

        /// <summary>
        /// Sets the effects volume.
        /// </summary>
        public void SetEffects()
        {
            _mixer.SetFloat("Effects", _effectsSlider.value);
            MenuPrefsSaver.Audio.SaveVolume(_masterSlider.value, _musicSlider.value, _effectsSlider.value);
        }
    }
}