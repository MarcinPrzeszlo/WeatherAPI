namespace WeatherApp.Models.WeatherModels

{
    public class ThingSpeakResponse
    {
        public Channel? Channel { get; set; }
        public List<Feed>? Feeds { get; set; }
    }
}
