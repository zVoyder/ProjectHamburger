namespace VUDK.Extensions
{
    using UnityEngine;
    using VUDK.Features.Main.AudioSystem.AudioObjects;

    public static class AudioExtension
    {
        /// <summary>
        /// Play clip at point in space.
        /// </summary>
        /// <param name="clip"><see cref="AudioClip"/> to play.</param>
        /// <param name="position">Position in space from where to play it.</param>
        public static AudioSFX PlayClipAtPoint(this AudioClip clip, Vector3 position)
        {
            AudioSFX sfx = AudioSFX.Create(clip);
            sfx.PlayAtPosition(position);
            return sfx;
        }
    }
}
