namespace WeatherAPI.Models
{
    public class CreateSensorDto
    {
        public string Type { get; set; }
        public string SerialNumber { get; set; }
        public string? Adress { get; set; }
        public double? Lat { get; set; }
        public double? Lon { get; set; }
        public int? UserId { get; set; }
    }
}
