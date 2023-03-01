namespace WeatherAPI.Models.OpenWeatherMapModels
{
    public class WeatherResponse
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Timezone { get; set; }
        public int TimezoneOffset { get; set; }
        public Current Current { get; set; }
        public List<Daily> Daily { get; set; }
    }
}
