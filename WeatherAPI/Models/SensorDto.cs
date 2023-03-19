namespace WeatherAPI.Models
{
    public class SensorDto
    {
        public string Type { get; set; }
        public string? Adress { get; set; }
        public double? Lat { get; set; }
        public double? Lon { get; set;}
    }
}
