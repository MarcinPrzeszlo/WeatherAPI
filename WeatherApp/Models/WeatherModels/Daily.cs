namespace WeatherApp.Models.WeatherModels
{
    public class Daily
    {
        public int Dt { get; set; }
        public int Sunrise { get; set; }
        public int Sunset { get; set; }
        public int Moonrise { get; set; }
        public int Moonset { get; set; }
        public float Moon_Phase { get; set; }
        public Temp? Temp { get; set; }
        public Feels_Like? Feels_Like { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public int Dew_Point { get; set; }
        public int Wind_Speed { get; set; }
        public int Wind_Deg { get; set; }
        public float Wind_Gust { get; set; }
        public List<Weather>? Weather { get; set; }
        public int Clouds { get; set; }
        public float Pop { get; set; }
        public float Rain { get; set; }
        public float Snow { get; set; }
        public float Uvi { get; set; }
    }
}
