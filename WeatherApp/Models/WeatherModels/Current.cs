namespace WeatherApp.Models.WeatherModels
{
    public class Current
    {
        public int Dt { get; set; }
        public int Sunrise { get; set; }
        public int Sunset { get; set; }
        public int Temp { get; set; }
        public int Feels_Like { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public int Dew_Point { get; set; }
        public float Uvi { get; set; }
        public int Clouds { get; set; }
        public int Visibility { get; set; }
        public float Wind_Speed { get; set; }
        public int Wind_Deg { get; set; }
        public List<Weather>? Weather { get; set; }


    }
}
