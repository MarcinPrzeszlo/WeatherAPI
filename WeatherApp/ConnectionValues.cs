namespace WeatherApp
{
    public class ConnectionValues
    {
        public string OPEN_WEATHER_APP_ID { get; set; }
        public string part { get; set; }
        public string limit { get; set; }
        public string ARDUINO_THINGSPEAK_API_KEY { get; set; }
        public string ARDUINO_CHANNEL_ID { get; set; }
        public string RASPBERRY_THINGSPEAK_API_KEY { get; set; }
        public string RASPBERRY_CHANNEL_ID { get; set; }
        public string JwtKey { get; set; }
        public int JwtExpireDays { get; set; }
        public string JwtIssuer { get; set; }
    }
}
