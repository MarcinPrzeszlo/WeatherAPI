using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherAPI.Models.OpenWeatherMapModels;
using WeatherAPI.Services;

namespace WeatherAPI.Controllers
{
    //[ApiController]
    [Authorize]
    [Route("weather")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IOpenWeatherMapServices _openWeatherMapService;
        public WeatherForecastController(IOpenWeatherMapServices openWeatherMapService)
        {
            _openWeatherMapService = openWeatherMapService;
        }

        [HttpGet]
        [Route("city")]
        public async Task<ActionResult> GetCoordinates([FromQuery]string cityName)
        {
            List<GeocodingResponse> geocodingResponse = await _openWeatherMapService.GetCoordinates(cityName);

            //return Ok(geocodingResponse[0]);
            return RedirectToAction("getForecast", geocodingResponse[0]);
            //zwiêkszyæ iloœæ prezentowanych wyników i daæ mo¿liwoœæ wyboru
        }

        [HttpGet]
        [Route("forecast")]
        public async Task<ActionResult> GetForecast(GeocodingResponse geocodingResponse)
        {
            var weatherResponse = await _openWeatherMapService.GetForecast(geocodingResponse);
            return Ok(weatherResponse);
        }
    }
}