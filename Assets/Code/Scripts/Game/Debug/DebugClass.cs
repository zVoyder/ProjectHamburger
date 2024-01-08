namespace ProjectH.Debug
{
    using ProjectH.Features.Moves.Undo;
    using UnityEngine;
    using UnityEngine.Audio;
    using VUDK.Features.Main.AudioSystem;
    using VUDK.Generic.Managers.Main;

    public class DebugClass : MonoBehaviour
    {
        [SerializeField]
        private AudioSourceSettings _clip;

        [ContextMenu("PlayClip")]
        public void Try()
        {
            MainManager.Ins.AudioManager.PlayPool(_clip);
        }
    }
}
