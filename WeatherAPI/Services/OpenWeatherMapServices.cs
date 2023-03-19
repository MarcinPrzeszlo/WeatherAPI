using System.Collections.Generic;
using System.Text.Json.Serialization;
using WeatherAPI.Models.OpenWeatherMapModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace WeatherAPI.Services
{

    public interface IOpenWeatherMapServices
    {
        Task<List<GeocodingResponse>> GetCoordinates(string cityName);
        Task<WeatherResponse> GetForecast(GeocodingResponse geocodingResponse);
    }

    public class OpenWeatherMapServices: IOpenWeatherMapServices
    {
        public static readonly HttpClient httpClient = new HttpClient();
        private readonly ConnectionValues _connectionValues;


        public OpenWeatherMapServices(ConnectionValues connectionValues)
        {
            _connectionValues = connectionValues;
        }
        
        public async Task<List<GeocodingResponse>> GetCoordinates(string cityName)
        {
            string AppId = _connectionValues.OpenWeatherAppId;
            string cityLimit = _connectionValues.GeocodingResponseNumberOfCities;
            
            var url = $"http://api.openweathermap.org/geo/1.0/direct?q={cityName}&limit={cityLimit}&appid={AppId}";
            var response =  await httpClient.GetAsync(url);
            string json = null;

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(message: "Response not succesful");
            }
            else
            {
                json = await response.Content.ReadAsStringAsync();
                if (json == "[]")
                {
                    throw new NullReferenceException(message: "Incorrect city name");
                }
            }

            var geocodingResponse = JsonConvert.DeserializeObject<List<GeocodingResponse>>(json);

            return geocodingResponse;
        }

        public async Task<WeatherResponse> GetForecast(GeocodingResponse geocodingResponse)
        {
            string AppId = _connectionValues.OpenWeatherAppId;
            string part = _connectionValues.WeatherForecastPartExclude;

            var url = $"https://api.openweathermap.org/data/3.0/onecall?lat={geocodingResponse.Lat}&lon={geocodingResponse.Lon}&exclude={part}&units=metric&appid={AppId}";
            var response = await httpClient.GetAsync(url);

            string json = null;

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Response not succesful");
            }
            else
            {
                json = await response.Content.ReadAsStringAsync();
                if (json == null)
                {
                    throw new NullReferenceException("Response content is null");
                }
            }

            var weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(json);
            return weatherResponse;

        }
    }
}
