using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WeatherApp.Models
{
    public class SearchByCityModel
    {
        [Required(ErrorMessage = "City name is empty")]
        [Display(Name = "City Name")]
        [StringLength(30, ErrorMessage = "Invalid Input,Length must be bettwen 1 to 30 characters")]
        public string? CityName { get; set; }
    }
}
