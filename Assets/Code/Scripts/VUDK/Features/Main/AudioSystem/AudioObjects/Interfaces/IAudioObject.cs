namespace VUDK.Features.Main.AudioSystem.AudioObjects.Interfaces
{
    using UnityEngine;

    public interface IAudioObject
    {
        /// <summary>
        /// Sets the audio clip to be played.
        /// </summary>
        /// <param name="clip"></param>
        public void SetClip(AudioClip clip);

        /// <summary>
        /// Plays the audio clip.
        /// </summary>
        public void Play();

        /// <summary>
        /// Stops the audio clip.
        /// </summary>
        public void Stop();
    }
}