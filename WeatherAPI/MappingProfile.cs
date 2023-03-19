using AutoMapper;
using WeatherAPI.Models;
using WeatherAPI.Entities;

namespace WeatherAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateSensorDto, Sensor>();
            CreateMap<UpdateSensorDto, Sensor>();
            CreateMap<Sensor, SensorDto>();


        }
    }
}
