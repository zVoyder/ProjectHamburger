namespace VUDK.Features.Main.AudioSystem.AudioObjects
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;

    public abstract class AudioControllerBase : MonoBehaviour
    {
        protected AudioManager AudioManager => MainManager.Ins.AudioManager;

        private void OnEnable() => RegisterAudioEvents();

        private void OnDisable() => UnregisterAudioEvents();

        /// <summary>
        /// Registers audio events.
        /// </summary>
        protected abstract void RegisterAudioEvents();

        /// <summary>
        /// Unregisters audio events.
        /// </summary>
        protected abstract void UnregisterAudioEvents();
    }
}
