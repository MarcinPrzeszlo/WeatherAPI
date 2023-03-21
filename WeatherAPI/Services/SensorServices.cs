using WeatherAPI.Entities;
using WeatherAPI.Models;
using WeatherAPI.Exceptions;
using WeatherAPI.Authorization;
using AutoMapper;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace WeatherAPI.Services
{
    public interface ISensorServices
    {
        void CreateSensor(CreateSensorDto dto);
        void DeleteSensor(int sensorId);
        void UpdateSensorDetails(int sensorId, UpdateSensorDto dto);
        PageResult<SensorDto> GetAllSensors(QueryModel query); 

    }

    public class SensorServices : ISensorServices
    {
        private readonly WeatherApiDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly IAuthorizationService _authorizationService;
        public SensorServices(WeatherApiDbContext dbContext, IMapper mapper, IUserContextService userContextService, IAuthorizationService authorizationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userContextService = userContextService;
            _authorizationService = authorizationService;

        }

        public void CreateSensor(CreateSensorDto dto)
        {
            var sensor = _dbContext.Sensors.FirstOrDefault(s => s.SerialNumber == dto.SerialNumber);
            if (sensor != null) 
            {
                throw new BadRequestException("This sensor already exist");
            }

            var sensorEntity = _mapper.Map<Sensor>(dto);
            _dbContext.Sensors.Add(sensorEntity);
            _dbContext.SaveChanges();
        }

        public void DeleteSensor(int sensorId)
        {
            var sensorEntity = GetSensorById(sensorId);

            _dbContext.Sensors.Remove(sensorEntity);
            _dbContext.SaveChanges();
        }

        public void UpdateSensorDetails(int sensorId, UpdateSensorDto dto)
        {
            var sensorEntity = GetSensorById(sensorId);

            sensorEntity.Adress = dto.Adress;
            sensorEntity.Lat = dto.Lat;
            sensorEntity.Lon = dto.Lon;

            var userId = _userContextService.GetUserId;
            sensorEntity.UserId = userId;
            _dbContext.SaveChanges();
        }

        public PageResult<SensorDto> GetAllSensors(QueryModel query)
        {
            var baseQuery = _dbContext.Sensors
                .Include(s => s.Owner)
                .Where(s => query.SearchPhrase == null || s.Adress.ToLower().Contains(query.SearchPhrase.ToLower()));

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnSelector = new Dictionary<string, Expression<Func<Sensor, object>>>()
                {
                    {nameof(Sensor.Adress), r => r.Adress},
                    {nameof(Sensor.Type), r => r.Type}
                };
                
                var selectedColumn = columnSelector[query.SortBy];
                baseQuery = query.SortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var sensors = baseQuery
                .Skip((query.PageNumber-1)*query.PageSize)
                .Take(query.PageSize)
                .ToList();

            var totalItemCount = sensors.Count;
            var sensorDtos = _mapper.Map<List<SensorDto>>(sensors);
            var pageResult = new PageResult<SensorDto>(sensorDtos, totalItemCount, query.PageSize, query.PageNumber);
            
            return pageResult;
        }



        private Sensor GetSensorById(int sensorId)
        {
            var sensorEntity = _dbContext.Sensors.FirstOrDefault(s => s.Id == sensorId);
            if (sensorEntity == null)
            {
                throw new BadRequestException($"Sensor with Id {sensorId} don't exist");
            }
            return sensorEntity;
        }

        
    }
}
