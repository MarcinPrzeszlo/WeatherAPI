using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using WeatherApp.Models.WeatherModels;

namespace WeatherApp.Models
{
    public class CityModel
    {
        public int Dt { get; set; }
        [Display(Name = "Weather forecast for ")]
        public string? Name { get; set; }
        public string? Country { get; set; }
        [Display(Name = "Temp.")]
        public float Temperature { get; set; }
        [Display(Name = "Humidity")]
        public int Humidity { get; set; }
        [Display(Name = "Pressure")]
        public int Pressure { get; set; }
        [Display(Name = "Wind")]
        public float Wind { get; set; }
        [Display(Name = "Weather Condition")]
        public string? Weather { get; set; }
        public int ID { get; set; }
        public List<Daily>? Daily { get; set; }
    }
}
