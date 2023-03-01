namespace WeatherApp.Models
{
    public class SensorModel
    {
        public List<string>? Ard_Created_At { get; set; }
        public List<float>? PM1 { get; set; }
        public List<float>? PM25 { get; set; }
        public List<float>? PM10 { get; set; }
        public List<float>? CO { get; set; }
        public List<float>? Benzene { get; set; }
        public List<string>? Raspb_Created_At { get; set; }
        public List<float>? Temperature { get; set; }
        public List<float>? Humidity { get; set; }
        public List<float>? Pressure { get; set; }
        public int Results { get; set; }
        public string? Location { get; set; }

    }
}
