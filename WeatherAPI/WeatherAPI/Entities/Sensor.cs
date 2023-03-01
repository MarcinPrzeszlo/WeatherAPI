namespace WeatherAPI.Entities
{
    public class Sensor
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Adress { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public virtual User Owner { get; set; }
    }
}
