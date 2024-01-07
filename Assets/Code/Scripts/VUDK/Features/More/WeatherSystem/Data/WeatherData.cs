namespace VUDK.Features.More.WeatherSystem.Data
{
    using System;

    [Serializable]
    public struct WeatherData
    {
        public Location location { get; set; }
        public Current current { get; set; }

        public override string ToString()
        {
            return $"Location:\n" +
                   $"{nameof(location.name)}: {location.name}\n" +
                   $"{nameof(location.region)}: {location.region}\n" +
                   $"{nameof(location.country)}: {location.country}\n" +
                   $"{nameof(location.lat)}: {location.lat}\n" +
                   $"{nameof(location.lon)}: {location.lon}\n" +
                   $"{nameof(location.tz_id)}: {location.tz_id}\n" +
                   $"{nameof(location.localtime_epoch)}: {location.localtime_epoch}\n" +
                   $"{nameof(location.localtime)}: {location.localtime}\n" +
                   $"\n{nameof(Current)}:\n" +
                   $"{nameof(current.last_updated_epoch)}: {current.last_updated_epoch}\n" +
                   $"{nameof(current.last_updated)}: {current.last_updated}\n" +
                   $"{nameof(current.temp_c)}: {current.temp_c}\n" +
                   $"{nameof(current.temp_f)}: {current.temp_f}\n" +
                   $"{nameof(current.is_day)}: {current.is_day}\n" +
                   $"\n{nameof(Condition)}:\n" +
                   $"{nameof(current.condition.text)}: {current.condition.text}\n" +
                   $"{nameof(current.condition.icon)}: {current.condition.icon}\n" +
                   $"{nameof(current.condition.code)}: {current.condition.code}\n" +
                   $"{nameof(current.wind_mph)}: {current.wind_mph}\n" +
                   $"{nameof(current.wind_kph)}: {current.wind_kph}\n" +
                   $"{nameof(current.wind_degree)}: {current.wind_degree}\n" +
                   $"{nameof(current.wind_dir)}: {current.wind_dir}\n" +
                   $"{nameof(current.pressure_mb)}: {current.pressure_mb}\n" +
                   $"{nameof(current.pressure_in)}: {current.pressure_in}\n" +
                   $"{nameof(current.precip_mm)}: {current.precip_mm}\n" +
                   $"{nameof(current.precip_in)}: {current.precip_in}\n" +
                   $"{nameof(current.humidity)}: {current.humidity}\n" +
                   $"{nameof(current.cloud)}: {current.cloud}\n" +
                   $"{nameof(current.feelslike_c)}: {current.feelslike_c}\n" +
                   $"{nameof(current.feelslike_f)}: {current.feelslike_f}\n" +
                   $"{nameof(current.vis_km)}: {current.vis_km}\n" +
                   $"{nameof(current.vis_miles)}: {current.vis_miles}\n" +
                   $"{nameof(current.uv)}: {current.uv}\n" +
                   $"{nameof(current.gust_mph)}: {current.gust_mph}\n" +
                   $"{nameof(current.gust_kph)}: {current.gust_kph}";
        }
    }

    [Serializable]
    public struct Location
    {
        public string name { get; set; }
        public string region { get; set; }
        public string country { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string tz_id { get; set; }
        public long localtime_epoch { get; set; }
        public string localtime { get; set; }
    }

    [Serializable]
    public struct Condition
    {
        public string text { get; set; }
        public string icon { get; set; }
        public int code { get; set; }
    }

    [Serializable]
    public struct Current
    {
        public long last_updated_epoch { get; set; }
        public string last_updated { get; set; }
        public double temp_c { get; set; }
        public double temp_f { get; set; }
        public int is_day { get; set; }
        public Condition condition { get; set; }
        public double wind_mph { get; set; }
        public double wind_kph { get; set; }
        public int wind_degree { get; set; }
        public string wind_dir { get; set; }
        public double pressure_mb { get; set; }
        public double pressure_in { get; set; }
        public double precip_mm { get; set; }
        public double precip_in { get; set; }
        public int humidity { get; set; }
        public int cloud { get; set; }
        public double feelslike_c { get; set; }
        public double feelslike_f { get; set; }
        public double vis_km { get; set; }
        public double vis_miles { get; set; }
        public double uv { get; set; }
        public double gust_mph { get; set; }
        public double gust_kph { get; set; }
    }
}