namespace VUDK.Features.UI.MenuSystem.MenuPreferences
{
    using UnityEngine;

    public static class MenuPrefsSaver
    {
        public static class Screen
        {
            public const string RESOLUTION_PREF = "RES";
            public const string FULLSCREEN_PREF = "FULLSCREEN";
            public const string MAXFPS_PREF = "MAXFPS";

            #region Save

            /// <summary>
            /// Saves the fullscreen setting to PlayerPrefs
            /// </summary>
            /// <param name="fullscreen">The fullscreen setting to save</param>
            public static void SaveFullscreen(bool fullscreen)
            {
                PlayerPrefs.SetString(FULLSCREEN_PREF, fullscreen ? "true" : "false");
            }

            /// <summary>
            /// Saves the resolution to PlayerPrefs
            /// </summary>
            /// <param name="resolution">The resolution to save</param>
            public static void SaveResolution(string resolution)
            {
                PlayerPrefs.SetString(RESOLUTION_PREF, resolution);
            }

            /// <summary>
            /// Saves the refresh rate to PlayerPrefs
            /// </summary>
            /// <param name="hz">The refresh rate to save</param>
            /// <param name="selectedHz">The selected refresh rate to save</param>
            public static void SaveRefreshRate(int hz, int selectedHz)
            {
                PlayerPrefs.SetString(MAXFPS_PREF, hz.ToString() + ":" + selectedHz.ToString());
            }

            #endregion

            #region Load

            /// <summary>
            /// Loads the fullscreen setting from PlayerPrefs
            /// </summary>
            /// <returns>The loaded fullscreen setting</returns>
            public static bool LoadFullscreen()
            {
                return PlayerPrefs.GetString(FULLSCREEN_PREF).Equals("true") ? true : false;
            }

            /// <summary>
            /// Loads the resolution from PlayerPrefs
            /// </summary>
            /// <param name="width">The width of the resolution</param>
            /// <param name="height">The height of the resolution</param>
            /// <param name="selectedValue">The selected resolution</param>
            /// <returns>True if the resolution was loaded successfully, false otherwise</returns>
            public static bool LoadResolution(out int width, out int height, out int selectedValue)
            {
                string resolutionString = PlayerPrefs.GetString(RESOLUTION_PREF);

                if (resolutionString != "")
                {
                    string[] res = resolutionString.Split(':');

                    width = int.Parse(res[0]);
                    height = int.Parse(res[1]);
                    selectedValue = int.Parse(res[2]);

                    return true;
                }

                width = 0;
                height = 0;
                selectedValue = 0;
                return false;
            }

            /// <summary>
            /// Save the maximum fps in playerprefs
            /// </summary>
            /// <param name="fps">Maximum fps</param>
            /// <param name="selectedFps">Selected fps</param>
            public static bool LoadRefreshRate(out int hz, out int selectedHz)
            {
                string s = PlayerPrefs.GetString(MAXFPS_PREF);

                if (s != "")
                {
                    string[] stringHz = s.Split(':');

                    hz = int.Parse(stringHz[0]);
                    selectedHz = int.Parse(stringHz[1]);

                    return true;
                }

                hz = 0;
                selectedHz = 0;
                return false;
            }

            #endregion
        }

        public static class Player
        {
            public const string PLAYER_ID = "PLAYER_ID";

            public static void SavePlayerID(string id)
            {
                PlayerPrefs.SetString(PLAYER_ID, id);
            }

            public static string LoadPlayerID()
            {
                return PlayerPrefs.GetString(PLAYER_ID);
            }
        }

        public static class Audio
        {
            public const string VOLUME_PREF = "VOLUME_PREF";

            /// <summary>
            /// Saves the audio volume in playerprefs.
            /// </summary>
            /// <param name="master">Master volume.</param>
            /// <param name="music">Music volume.</param>
            /// <param name="effects">Effects volume.</param>
            public static void SaveVolume(float master, float music, float effects)
            {
                PlayerPrefs.SetString(VOLUME_PREF, master + ":" + music + ":" + effects);
            }

            /// <summary>
            /// Loads the audio volume from playerprefs.
            /// </summary>
            /// <param name="master">Master volume.</param>
            /// <param name="music">Music volume.</param>
            /// <param name="effects">Effects volume.</param>
            /// <returns>True if the volume are loaded successfully, False otherwise.</returns>
            public static bool LoadVolume(out float master, out float music, out float effects)
            {
                string volString = PlayerPrefs.GetString(VOLUME_PREF);

                if (volString != "")
                {
                    string[] v = volString.Split(':');

                    master = float.Parse(v[0]);
                    music = float.Parse(v[1]);
                    effects = float.Parse(v[2]);

                    return true;
                }

                master = 0f;
                music = 0f;
                effects = 0f;
                return false;
            }
        }
    }
}