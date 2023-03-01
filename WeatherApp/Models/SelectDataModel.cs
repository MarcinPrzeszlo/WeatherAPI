using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Models
{
    public class SelectDataModel
    {
        [Range(1, 100000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Results { get; set; }
        public string? Location { get; set; }
    }
}
