namespace WeatherApp.Models.WeatherModels

{
    public class WeatherResponse
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Timezone { get; set; }
        public string TimezoneOffset { get; set; }
        public Current Current { get; set; }
        public List<Daily> Daily  { get; set; }
    }
}
