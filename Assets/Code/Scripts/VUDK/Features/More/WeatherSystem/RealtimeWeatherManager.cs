namespace VUDK.Features.More.WeatherSystem
{
    using UnityEngine;
    using UnityEngine.Events;
    using VUDK.Generic.Serializable;
    using VUDK.Features.More.APISystem.Data;
    using VUDK.Features.More.WeatherSystem.Data;
    using VUDK.Features.More.WeatherSystem.Enums;
    using VUDK.Features.More.WeatherSystem.Events;

    /// <summary>
    /// Manages real-time weather events in the game based on data retrieved from a weather API.
    /// Visit https://www.weatherapi.com or https://rapidapi.com/weatherapi/api/weatherapi-com
    /// for more information, documentation or to obtain API keys.
    /// </summary>
    public class RealtimeWeatherManager : MonoBehaviour
    {
        [Header("API Settings")]
        [Tooltip("API-KEYS to get weather data.")]
        [SerializeField]
        private APIPackageData _APIPackage;

        [Header("Query")]
        [Tooltip(
            "Query parameter based on which data is sent back.\n" +
            "It could be following:\r\n\r\n" +
            "Latitude and Longitude (Decimal degree) e.g: q=48.8567,2.3508\r\n" +
            "city name e.g.: q=Paris\r\n" +
            "US zip e.g.: q=10001\r\n" +
            "UK postcode e.g: q=SW1\r\n" +
            "Canada postal code e.g: q=G2J\r\n" +
            "metar:<metar code> e.g: q=metar:EGLL\r\n" +
            "iata:<3 digit airport code> e.g: q=iata:DXB\r\n" +
            "auto:ip IP lookup e.g: q=auto:ip\r\n" +
            "IP address (IPv4 and IPv6 supported) e.g: q=100.0.0.1\r\n" +
            "By ID returned from Search API. e.g: q=id:2801268.")]
        [SerializeField]
        private string _weatherLocationQuery;

        [Header("Time Update")]
        [Tooltip("Time in hours to update the weather.")]
        [SerializeField, Range(1, 10)]
        private int _requestUpdatePeriod;

        [Header("Major Weather Events")]
        public UnityEvent OnClear;
        public UnityEvent OnCloudy;
        public UnityEvent OnRain;
        public UnityEvent OnSnow;
        public UnityEvent OnFog;

        [Header("Specific Weather Events")]
        [SerializeField]
        private SerializableDictionary<WeatherConditionType, UnityEvent> _weatherEvents;

        [Header("Day Night Events")]
        public UnityEvent OnDay;
        public UnityEvent OnNight;

        [Header("Default Settings")]
        [SerializeField]
        private WeatherConditionType _defaultWeatherCondition = WeatherConditionType.Clear;
        [SerializeField]
        private bool _defaultIsDay;

        private DelayTask _timeUpdateDelay;

        private void Awake()
        {
            _timeUpdateDelay = new DelayTask(_requestUpdatePeriod * 3600f);
            UpdateInGameWeather();
        }

        private void OnEnable()
        {
            _timeUpdateDelay.OnTaskCompleted += UpdateInGameWeather;
        }

        private void OnDisable()
        {
            _timeUpdateDelay.OnTaskCompleted -= UpdateInGameWeather;
        }

        private void Update()
        {
            _timeUpdateDelay.Process();
        }

        /// <summary>
        /// Updates the weather in the game.
        /// </summary>
        [ContextMenu("Update Weather")]
        private void UpdateInGameWeather()
        {
            WeatherCalculator.GetWeather(_APIPackage, _weatherLocationQuery,
                onReceivedWeatherData: TriggerWeatherEvent,
                onFailedToReceiveWeatherData: TriggerDefaults);
            _timeUpdateDelay.Start();
        }

        /// <summary>
        /// Triggers the weather event.
        /// </summary>
        /// <param name="weatherData">Weather data to trigger.</param>
        private void TriggerWeatherEvent(WeatherData weatherData)
        {
#if UNITY_EDITOR
            UnityEngine.Debug.Log($"Triggering Weather: {weatherData}");
#endif
            WeatherEvents.OnWeatherChanged?.Invoke(weatherData);
            WeatherConditionType condition = (WeatherConditionType)weatherData.current.condition.code;
            TriggerDayNightEvent(weatherData.current.is_day == 1);
            TriggerMajorWeatherEvent(condition);
            TriggerSpecificWeatherEvent(condition);
        }

        /// <summary>
        /// Triggers the day/night event.
        /// </summary>
        /// <param name="isDay">True if is Day, False if not.</param>
        private void TriggerDayNightEvent(bool isDay)
        {
            if (isDay)
                OnDay?.Invoke();
            else
                OnNight?.Invoke();
        }

        /// <summary>
        /// Triggers the major weather event.
        /// </summary>
        /// <param name="weatherCondition">Weather condition to trigger.</param>
        private void TriggerMajorWeatherEvent(WeatherConditionType weatherCondition)
        {
            switch (weatherCondition)
            {
                case WeatherConditionType.Cloudy:
                case WeatherConditionType.PartlyCloudy:
                case WeatherConditionType.Overcast:
                    OnCloudy?.Invoke();
                    break;

                case WeatherConditionType.LightRain:
                case WeatherConditionType.ModerateRainAtTimes:
                case WeatherConditionType.ModerateRain:
                case WeatherConditionType.HeavyRainAtTimes:
                case WeatherConditionType.HeavyRain:
                case WeatherConditionType.PatchyRainPossible:
                case WeatherConditionType.ModerateOrHeavyRainShower:
                case WeatherConditionType.TorrentialRainShower:
                case WeatherConditionType.LightRainShower:
                case WeatherConditionType.ModerateOrHeavyRainWithThunder:
                case WeatherConditionType.PatchyLightRainWithThunder:
                case WeatherConditionType.LightFreezingRain:
                case WeatherConditionType.PatchyLightDrizzle:
                case WeatherConditionType.LightDrizzle:
                case WeatherConditionType.FreezingDrizzle:
                case WeatherConditionType.HeavyFreezingDrizzle:
                case WeatherConditionType.PatchyFreezingDrizzlePossible:
                    OnRain?.Invoke();
                    break;

                case WeatherConditionType.LightSnow:
                case WeatherConditionType.PatchyLightSnow:
                case WeatherConditionType.ModerateSnow:
                case WeatherConditionType.PatchyModerateSnow:
                case WeatherConditionType.HeavySnow:
                case WeatherConditionType.PatchyHeavySnow:
                case WeatherConditionType.LightSleet:
                case WeatherConditionType.ModerateOrHeavySleet:
                case WeatherConditionType.LightSleetShowers:
                case WeatherConditionType.ModerateOrHeavySleetShowers:
                case WeatherConditionType.LightSnowShowers:
                case WeatherConditionType.ModerateOrHeavySnowShowers:
                case WeatherConditionType.PatchySnowPossible:
                case WeatherConditionType.PatchySleetPossible:
                case WeatherConditionType.Blizzard:
                case WeatherConditionType.BlowingSnow:
                    OnSnow?.Invoke();
                    break;

                case WeatherConditionType.Fog:
                case WeatherConditionType.FreezingFog:
                case WeatherConditionType.Mist:
                    OnFog?.Invoke();
                    break;

                default:
                    OnClear?.Invoke();
                    break;
            }
        }

        /// <summary>
        /// Triggers the specific weather event.
        /// </summary>
        /// <param name="weatherCondition">Weather condition to trigger.</param>
        private void TriggerSpecificWeatherEvent(WeatherConditionType weatherCondition)
        {
            if (_weatherEvents.ContainsKey(weatherCondition))
                _weatherEvents[weatherCondition]?.Invoke();
        }

        /// <summary>
        /// Triggers the default weather events.
        /// </summary>
        private void TriggerDefaults()
        {
            TriggerDayNightEvent(_defaultIsDay);
            TriggerMajorWeatherEvent(_defaultWeatherCondition);
            TriggerSpecificWeatherEvent(_defaultWeatherCondition);
        }
    }
}