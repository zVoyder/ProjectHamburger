namespace VUDK.Features.Main.Camera.CameraModifiers
{
    using System;
    using System.Collections;
    using UnityEngine;

    public class CameraFade : CameraModifierBase
    {
        public enum FadeType
        {
            None,
            FadeIn,
            FadeOut,
            FadeInFadeOut,
            FadeOutFadeIn
        }

        [Header("Fade Settings")]
        [SerializeField, Tooltip("Fade duration time in seconds"), Min(0f)]
        private float _fadeDuration = 1f;
        [SerializeField, Tooltip("Fade color")]
        private Color _fadeColor;
        [SerializeField, Tooltip("Fade on Start")]
        private FadeType _fadeStart = FadeType.None;

        private float _alpha = 0f;

        public event Action OnFadeInStart;
        public event Action OnFadeInEnd;
        public event Action OnFadeOutStart;
        public event Action OnFadeOutEnd;

        public bool IsFading { get; private set; }

        private void Start()
        {
            if (_fadeStart == FadeType.None)
                return;

            switch (_fadeStart)
            {
                case FadeType.FadeIn:
                    DoFadeIn(_fadeDuration);
                    break;

                case FadeType.FadeOut:
                    DoFadeOut(_fadeDuration);
                    break;

                case FadeType.FadeInFadeOut:
                    DoFadeInOut(_fadeDuration);
                    break;

                case FadeType.FadeOutFadeIn:
                    DoFadeOutIn(_fadeDuration);
                    break;
            }
        }

        /// <summary>
        /// Starts fade out effect.
        /// </summary>
        /// <param name="time">time in seconds.</param>
        public void DoFadeOut(float time)
        {
            StartCoroutine(FadeOutRoutine(time));
        }

        /// <summary>
        /// Starts fade out effect.
        /// </summary>
        public void DoFadeOut()
        {
            DoFadeOut(_fadeDuration);
        }

        /// <summary>
        /// Starts fade in effect.
        /// </summary>
        /// <param name="time">time in seconds.</param>
        public void DoFadeIn(float time)
        {
            StartCoroutine(FadeInRoutine(time));
        }

        /// <summary>
        /// Starts fade in effect.
        /// </summary>
        public void DoFadeIn()
        {
            DoFadeIn(_fadeDuration);
        }

        /// <summary>
        /// Starts fade out followed by fade in.
        /// </summary>
        /// <param name="time">time in seconds.</param>
        public void DoFadeOutIn(float time)
        {
            StartCoroutine(FadeOutInRoutine(time));
        }

        /// <summary>
        /// Starts fade out followed by fade in.
        /// </summary>
        public void DoFadeOutInt()
        {
            DoFadeInOut(_fadeDuration);
        }

        /// <summary>
        /// Starts fade in followed by fade out.
        /// </summary>
        /// <param name="time">time in seconds.</param>
        public void DoFadeInOut(float time)
        {
            StartCoroutine(FadeInOutRoutine(time));
        }

        /// <summary>
        /// Starts fade in followed by fade out.
        /// </summary>
        public void DoFadeInOut()
        {
            DoFadeInOut(_fadeDuration);
        }

        /// <summary>
        /// Coroutine fading in followed by fading out.
        /// </summary>
        /// <param name="time">time in seconds.</param>
        private IEnumerator FadeInOutRoutine(float time)
        {
            yield return FadeInRoutine(time);
            yield return FadeOutRoutine(time);
        }

        /// <summary>
        /// Coroutine fading out followed by fading in.
        /// </summary>
        /// <param name="time">time in seconds.</param>
        private IEnumerator FadeOutInRoutine(float time)
        {
            yield return FadeOutRoutine(time);
            yield return FadeInRoutine(time);
        }

        /// <summary>
        /// Coroutine fading out.
        /// </summary>
        /// <param name="time">time in seconds.</param>
        private IEnumerator FadeOutRoutine(float time)
        {
            if (IsFading) yield break;

            OnFadeOutStart?.Invoke();
            IsFading = true;

            float startAlpha = 1f; // Alpha is 1 so the FadeOut start with a DrawTexture already fully visible
            float progress = 0f;

            // Continuously update the alpha value while the progress is less than 1.
            while (progress < 1f)
            {
                progress = Mathf.Clamp01(progress + Time.deltaTime / time); // clamping the progress value to the range of 0 to 1, the fading effect is always controlled and never exceeds the desired fadeDuration
                _alpha = Mathf.Lerp(startAlpha, 0f, progress);
                yield return null;
            }

            IsFading = false;
            OnFadeOutEnd?.Invoke();
        }

        /// <summary>
        /// Coroutine fading in.
        /// </summary>
        /// <param name="time">time in seconds.</param>
        private IEnumerator FadeInRoutine(float time)
        {
            if (IsFading) yield break;

            OnFadeInStart?.Invoke();
            IsFading = true;

            float startAlpha = 0f; // Alpha is 0 because the FadeIn start with a DrawTexture invisible
            float progress = 0f;

            while (progress < 1f)
            {
                progress = Mathf.Clamp01(progress + Time.deltaTime / time);
                _alpha = Mathf.Lerp(startAlpha, 1f, progress);
                yield return null;
            }

            IsFading = false;
            OnFadeInEnd?.Invoke();
        }

        private void OnGUI()
        {
            GUI.color = new Color(_fadeColor.r, _fadeColor.g, _fadeColor.b, _alpha);
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
        }
    }
}