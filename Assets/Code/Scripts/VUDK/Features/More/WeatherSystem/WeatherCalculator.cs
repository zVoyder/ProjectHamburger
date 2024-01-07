namespace VUDK.Features.More.WeatherSystem
{
    using System;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Networking;
    using VUDK.Features.More.APISystem.Data;
    using VUDK.Features.More.WeatherSystem.Data;

    public static class WeatherCalculator
    {
        public static class Errors
        {
            public const int APIInvalid = 401;
            public const int APIExceededCallsPerMonthQuota = 403;
            public const int InvalidQueryOrURL = 400;
            public const int TooManyRequests = 429;
        }

        /// <summary>
        /// Retrieves weather data using the provided APIPackageData and query, with callbacks for success and failure.
        /// </summary>
        /// <param name="apipackage">APIPackageData containing API keys.</param>
        /// <param name="query">The location query for weather data.</param>
        /// <param name="onReceivedWeatherData">Callback for successful retrieval of weather data.</param>
        /// <param name="onFailedToReceiveWeatherData">Callback for failed retrieval of weather data.</param>
        public static void GetWeather(APIPackageData apipackage, string query, Action<WeatherData> onReceivedWeatherData, Action onFailedToReceiveWeatherData)
        {
            GetWeather(apipackage.APIS, query, onReceivedWeatherData, onFailedToReceiveWeatherData);
        }

        /// <summary>
        /// Retrieves weather data using the provided API keys, location query, with callbacks for success and failure.
        /// </summary>
        /// <param name="apikeys">Array of API keys for accessing the weather API.</param>
        /// <param name="query">The location query for weather data.</param>
        /// <param name="onReceivedWeatherData">Callback for successful retrieval of weather data.</param>
        /// <param name="onFailedToReceivedWeatherData">Callback for failed retrieval of weather data.</param>
        public static void GetWeather(string[] apikeys, string query, Action<WeatherData> onReceivedWeatherData, Action onFailedToReceivedWeatherData)
        {
            string url = $"https://weatherapi-com.p.rapidapi.com/current.json?q={query}";

            UnityWebRequest request = UnityWebRequest.Get(url);

            if (apikeys.Length == 0)
            {
                UnityEngine.Debug.LogWarning("No Valid API-KEYS found.");
                onFailedToReceivedWeatherData?.Invoke();
                return;
            }

            request.SetRequestHeader("X-RapidAPI-Key", apikeys[0]);
            request.SetRequestHeader("X-RapidAPI-Host", "weatherapi-com.p.rapidapi.com");

            var operation = request.SendWebRequest();

            operation.completed += (AsyncOperation obj) =>
            {
                if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
                {
                    if( request.responseCode == Errors.APIInvalid || 
                        request.responseCode == Errors.APIExceededCallsPerMonthQuota || 
                        request.responseCode == Errors.TooManyRequests)
                    {
                        UnityEngine.Debug.LogWarning($"Error with API-KEY {apikeys[0]}: {request.error}.\n Trying Next API-KEY.");
                        GetWeather(apikeys.Skip(1).ToArray(), query, onReceivedWeatherData, onFailedToReceivedWeatherData);
                        return;
                    }

                    UnityEngine.Debug.LogWarning($"Error: {request.error}");
                    onFailedToReceivedWeatherData?.Invoke();
                }
                else
                {
                    string body = request.downloadHandler.text;
                    WeatherData weatherData = JsonUtility.FromJson<WeatherData>(body);
                    onReceivedWeatherData?.Invoke(weatherData);
                }
            };
        }
    }
}