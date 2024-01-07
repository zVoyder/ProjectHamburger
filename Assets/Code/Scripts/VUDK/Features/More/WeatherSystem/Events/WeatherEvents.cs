namespace VUDK.Features.More.WeatherSystem.Events
{
    using System;
    using VUDK.Features.More.WeatherSystem.Data;

    public static class WeatherEvents
    {
        public static Action<WeatherData> OnWeatherChanged;
    }
}