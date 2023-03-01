namespace WeatherApp.Models.WeatherModels
{
    public class Feed
    {
        public string? Created_At { get; set; }
        public int Entry_Id { get; set; }
        public float Field1 { get; set; } // PM1
        public float Field2 { get; set; } // PM2
        public float Field3 { get; set; } // PM10
        public float Field4 { get; set; } // CO
        public float Field5 { get; set; } // Benzene
        public float Field6 { get; set; } // Temperature
        public float Field7 { get; set; } // Humidity
        public float Field8 { get; set; } // Pressure

    }
}
