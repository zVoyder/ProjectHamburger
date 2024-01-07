namespace VUDK.Features.UI.MenuSystem
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using VUDK.Features.UI.MenuSystem.MenuPreferences;

    public class UIScreenSettings : MonoBehaviour
    {
        [SerializeField, Header("Dropdown")]
        private TMP_Dropdown _dropResolution;
        [SerializeField]
        private TMP_Dropdown _dropFPS;

        [SerializeField, Header("Toggle")]
        private Toggle _toggleFullscreen;

        public bool Fullscreen { get; private set; }

        private void Awake()
        {
            QualitySettings.vSyncCount = 0; // Disable V-Sync to allow the FPS Cap

            foreach (string resolution in GetCurrentResolutions())
            {
                _dropResolution.options.Add(new TMP_Dropdown.OptionData(resolution));
            }

            _toggleFullscreen.SetIsOnWithoutNotify(MenuPrefsSaver.Screen.LoadFullscreen());

            if (MenuPrefsSaver.Screen.LoadResolution(out int w, out int h, out int sel))
            {
                _dropResolution.value = sel;
                Screen.SetResolution(w, h, _toggleFullscreen.isOn);
            }
            else
            {
                Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, _toggleFullscreen.isOn);
            }

            if (MenuPrefsSaver.Screen.LoadRefreshRate(out int hz, out int selectedHz))
            {
                Application.targetFrameRate = hz;
                _dropFPS.value = selectedHz;
            }
        }

        /// <summary>
        /// Gets the current available resolutions
        /// as array of strings.
        /// </summary>
        /// <returns>Available resolutions.</returns>
        private string[] GetCurrentResolutions()
        {
            Resolution[] resolutions = Screen.resolutions;
            System.Array.Reverse(resolutions);

            List<string> resList = new List<string>(); // Creating a list of strings

            foreach (Resolution res in resolutions) // In this list of string only add the witdth and the height
            {
                resList.Add(res.width + "x" + res.height);
            }

            return resList.ToArray().Distinct().ToArray(); // Distinct() because there are resolutions' duplicates due the refresh rate
        }

        #region Setter

        /// <summary>
        /// Sets the resolution of the screen.
        /// </summary>
        public void SetResolution()
        {
            string[] res = _dropResolution.options[_dropResolution.value].text.Split('x');

            int width = int.Parse(res[0]);
            int height = int.Parse(res[1]);

            MenuPrefsSaver.Screen.SaveResolution(width + ":" + height + ":" + _dropResolution.value);

            Screen.SetResolution(width, height, Fullscreen);
        }

        /// <summary>
        /// Sets the refresh rate of the screen.
        /// </summary>
        public void SetRefreshRate()
        {
            string hz = _dropFPS.options[_dropFPS.value].text;
            hz = System.Text.RegularExpressions.Regex.Replace(hz, "[^0-9]", ""); // Regular expression

            int fps = int.Parse(hz);

            MenuPrefsSaver.Screen.SaveRefreshRate(fps, _dropFPS.value);
            Application.targetFrameRate = fps;
        }

        /// <summary>
        /// Toggle for setting the prefered fullscreen mode
        /// </summary>
        public void ToggleSetFullScreen()
        {
            Screen.fullScreen = _toggleFullscreen.isOn;
            MenuPrefsSaver.Screen.SaveFullscreen(_toggleFullscreen.isOn);
        }

        #endregion
    }
}
