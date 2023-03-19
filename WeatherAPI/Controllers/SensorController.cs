using Microsoft.AspNetCore.Mvc;
using WeatherAPI.Models;
using WeatherAPI.Services;

namespace WeatherAPI.Controllers
{
    [Route("api/sensor")]
    public class SensorController : ControllerBase
    {
        private readonly ISensorServices _sensorService;
        public SensorController(ISensorServices sensorServices)
        {
            _sensorService = sensorServices;
        }


        //admin
        [HttpPost]
        public ActionResult CreateSensor([FromBody] CreateSensorDto dto)
        {
            _sensorService.CreateSensor(dto);
            return Ok();
        }

        //admin
        [HttpDelete]
        public ActionResult DeleteSensor([FromQuery]int sensorId)
        {
            _sensorService.DeleteSensor(sensorId);
            return NoContent();
        }

        [HttpPut]
        public ActionResult UpdateSensorDetails([FromQuery] int sensorId, [FromBody] UpdateSensorDto dto)
        {
            _sensorService.UpdateSensorDetails(sensorId, dto);
            return Ok();
        }

        [HttpGet]
        public ActionResult GetAllSensors([FromQuery] QueryModel query)
        {
            var sensorDtos = _sensorService.GetAllSensors(query);
            return Ok(sensorDtos);
        }

    }
}
