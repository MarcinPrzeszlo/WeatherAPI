namespace WeatherAPI.Entities
{
    public class SensorReading
    {
        public int Id { get; set; }
        public int PM1 { get; set; }
        public int PM2_5 { get; set; }
        public int PM10 { get; set; }
        public float Temperature { get; set; }
        public int Humidity { get; set; }
        public int Pressure { get; set; }
        public int? NO2 { get; set; }
        public int? O3 { get; set; }
        public int? SO2 { get; set; }
        public int? CO { get; set; }
        public int SensorId { get; set; }
        public virtual Sensor Sensor { get; set; }


    }
}
