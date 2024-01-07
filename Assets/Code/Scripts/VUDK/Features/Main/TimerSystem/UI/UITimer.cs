namespace VUDK.Features.Main.TimerSystem.UI
{
    using UnityEngine;
    using TMPro;
    using VUDK.Constants;
    using VUDK.Features.Main.EventSystem;
    using System;
    using VUDK.Features.Main.TimerSystem.Events;

    public class UITimer : MonoBehaviour
    {
        private enum TimerFormat
        {
            HHMMSS,
            MMSS,
            SS
        }

        [Header("Timer Settings")]
        [SerializeField]
        private TimerFormat _format;
        [SerializeField]
        private RectTransform _timerBox;
        [SerializeField]
        private TMP_Text _text;

        private void Awake()
        {
            Disable();
        }

        private void OnEnable()
        {
            TimerEvents.OnTimerStart += Enable;
            TimerEvents.OnTimerStop += Disable;
            TimerEvents.OnTimerCount += UpdateTimerText;
        }

        private void OnDisable()
        {
            TimerEvents.OnTimerStart -= Enable;
            TimerEvents.OnTimerStop -= Disable;
            TimerEvents.OnTimerCount -= UpdateTimerText;
        }

        public void Enable()
        {
            _timerBox.gameObject.SetActive(true);
        }

        public void Disable()
        {
            _timerBox.gameObject.SetActive(false);
        }

        private void UpdateTimerText(int time)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(time);
            _text.text = FormatTimeSpan(timeSpan);
        }

        private string FormatTimeSpan(TimeSpan timeSpan)
        {
            switch (_format)
            {
                case TimerFormat.HHMMSS:
                    return $"{(int)timeSpan.TotalHours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}"; // D2 = 2 digits
                case TimerFormat.MMSS:
                    return $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
                case TimerFormat.SS:
                    return $"{timeSpan.TotalSeconds:D2}";
            }

            return string.Empty;
        }
    }
}