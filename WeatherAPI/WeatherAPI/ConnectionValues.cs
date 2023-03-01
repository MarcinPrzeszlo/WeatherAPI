namespace WeatherAPI
{
    public class ConnectionValues
    {
        public string  OpenWeatherAppId { get; set; }
        public string WeatherForecastPartExclude { get; set; }
        public string GeocodingResponseNumberOfCities { get; set; }
        public string JwtKey { get; set; }
        public int ExpireDays { get; set; }
        public string JwtIssuer { get; set; }
    }
}
 