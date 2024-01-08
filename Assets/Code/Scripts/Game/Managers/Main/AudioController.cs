namespace ProjectH.Managers.Main
{
    using ProjectH.Constants;
    using UnityEngine;
    using VUDK.Features.Main.AudioSystem;
    using VUDK.Features.Main.AudioSystem.AudioObjects;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Generic.Serializable;
    using VUDKEventKeys = VUDK.Constants.EventKeys;

    public class AudioController : AudioControllerBase
    {
        [Header("Game Audio Clips")]
        [SerializeField]
        private AudioSourceSettings _swipeEffect;
        [SerializeField]
        private AudioSourceSettings _eatEffect;
        [SerializeField]
        private AudioSourceSettings _victoryEffect;

        [Header("UI Audio Clips")]
        [SerializeField]
        private AudioClip _buttonEffect;

        protected override void RegisterAudioEvents()
        {
            EventManager.Ins.AddListener(EventKeys.PieceEvents.OnMoveAnimationStarted, PlaySwipeEffect);
            EventManager.Ins.AddListener(EventKeys.GameEvents.OnGameVictory, PlayVictoryEffect);
            EventManager.Ins.AddListener(VUDKEventKeys.UIEvents.OnButtonPressed, PlayButtonEffect);
            EventManager.Ins.AddListener(EventKeys.GameEvents.OnEatTapped, PlayEatEffect);
        }

        protected override void UnregisterAudioEvents()
        {
            EventManager.Ins.RemoveListener(EventKeys.PieceEvents.OnMoveAnimationStarted, PlaySwipeEffect);
            EventManager.Ins.RemoveListener(EventKeys.GameEvents.OnGameVictory, PlayVictoryEffect);
            EventManager.Ins.RemoveListener(VUDKEventKeys.UIEvents.OnButtonPressed, PlayButtonEffect);
            EventManager.Ins.RemoveListener(EventKeys.GameEvents.OnEatTapped, PlayEatEffect);
        }

        /// <summary>
        /// Plays swipe audio effect.
        /// </summary>
        private void PlaySwipeEffect()
        {
            AudioManager.PlayPool(_swipeEffect);
        }

        /// <summary>
        /// Plays eat audio effect.
        /// </summary>
        private void PlayEatEffect()
        {
            AudioManager.PlayPool(_eatEffect);
        }

        /// <summary>
        /// Plays victory audio effect.
        /// </summary>
        private void PlayVictoryEffect()
        {
            AudioManager.PlayMain(_victoryEffect);
        }

        /// <summary>
        /// Plays button audio effect.
        /// </summary>
        private void PlayButtonEffect()
        {
            AudioManager.PlayMain(_buttonEffect);
        }
    }
}