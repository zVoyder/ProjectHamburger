namespace VUDK.Features.More.WeatherSystem.Debug
{
    using TMPro;
    using UnityEngine;
    using VUDK.Features.More.WeatherSystem.Data;
    using VUDK.Features.More.WeatherSystem.Events;

    [RequireComponent(typeof(TMP_Text))]
    public class DebugWeatherLogger : MonoBehaviour
    {
        private TMP_Text _text;

        private void Awake()
        {
            TryGetComponent(out _text);
        }

        private void OnEnable()
        {
            WeatherEvents.OnWeatherChanged += OnWeatherChanged;
        }

        private void OnDisable()
        {
            WeatherEvents.OnWeatherChanged -= OnWeatherChanged;
        }

        /// <summary>
        /// Called when the weather changes.
        /// </summary>
        /// <param name="data">Weather data.</param>
        private void OnWeatherChanged(WeatherData data)
        {
            _text.text = $"Weather:\n{data}";
        }
    }
}