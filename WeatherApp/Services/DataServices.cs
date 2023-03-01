using System.Drawing;
using WeatherApp.Models;
using WeatherApp.Models.WeatherModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using WeatherApp.Exceptions;

namespace WeatherApp.Services
{
    public interface IDataService
    {
        Task<Coord> GetCityCoord(string cityName);
        Task<CityModel> GetForecast(Coord coord);
        Task<SensorModel> GetMeasurements(SelectDataModel model);
    }

    public class DataServices : IDataService
    {
        public static readonly RestClient client = new();

        private readonly ConnectionValues _connectionValues;
        public DataServices(ConnectionValues connectionValues)
        {
            _connectionValues = connectionValues;
        }

        public async Task<Coord> GetCityCoord(string cityName)
        {
            string APP_ID = _connectionValues.OPEN_WEATHER_APP_ID;
            string limit = _connectionValues.limit;

            var url = $"http://api.openweathermap.org/geo/1.0/direct?q={cityName}&limit={limit}&appid={APP_ID}";
            Coord coord = null;



            var request = new RestRequest(url, Method.Get);
            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                throw new ConnectionFailedException("Response is not succesful");
            }
            else
            {
                if (response.Content == "[]")
                {
                    //throw new NotFoundException("Data not found");
                    return null;
                };
            }

            var content = JsonConvert.DeserializeObject<JToken>(response.Content);
            var GeocodingResponse = content.ToObject<List<GeocodingResponse>>();
            coord = new Coord()
            {
                Lat = GeocodingResponse[0].Lat,
                Lon = GeocodingResponse[0].Lon,
                Name = GeocodingResponse[0].Name,
                Country = GeocodingResponse[0].Country
            };

            return coord;
        }


        public async Task<CityModel> GetForecast(Coord coord)
        {
            double lat = coord.Lat;
            double lon = coord.Lon;

            string APP_ID = _connectionValues.OPEN_WEATHER_APP_ID;
            string part = _connectionValues.part;
            var url = $"https://api.openweathermap.org/data/3.0/onecall?lat={lat}&lon={lon}&exclude={part}&units=metric&appid={APP_ID}";
            var request = new RestRequest(url, Method.Get);
            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                throw new ConnectionFailedException("Response is not succesful");
            }
            else
            {
                var content = JsonConvert.DeserializeObject<JToken>(response.Content);
                var weatherResponse = content.ToObject<WeatherResponse>();

                if (weatherResponse != null)
                {
                    var viewModel = new CityModel();
                    viewModel.Name = coord.Name;
                    viewModel.Country = coord.Country;
                    viewModel.Dt = weatherResponse.Current.Dt;
                    viewModel.Temperature = weatherResponse.Current.Temp;
                    viewModel.Humidity = weatherResponse.Current.Humidity;
                    viewModel.Pressure = weatherResponse.Current.Pressure;
                    viewModel.Weather = weatherResponse.Current.Weather[0].Main;
                    viewModel.ID = weatherResponse.Current.Weather[0].Id;
                    viewModel.Wind = weatherResponse.Current.Wind_Speed;
                    viewModel.Daily = weatherResponse.Daily;

                    return viewModel;
                }
                else
                {
                    throw new NotFoundException("Data not found");
                };
            }
        }


        public async Task<SensorModel> GetMeasurements(SelectDataModel model)
        {

            string API_KEY_A = _connectionValues.ARDUINO_THINGSPEAK_API_KEY;
            string channelID_A = _connectionValues.ARDUINO_CHANNEL_ID;
            var url_A = $"https://api.thingspeak.com/channels/{channelID_A}/feeds.json?api_key={API_KEY_A}&results={model.Results}";
            var request_A = new RestRequest(url_A, Method.Get);
            var response_A = await client.ExecuteAsync(request_A);

            string API_KEY_R = _connectionValues.RASPBERRY_THINGSPEAK_API_KEY;
            string channelID_R = _connectionValues.RASPBERRY_CHANNEL_ID;
            var url_R = $"https://api.thingspeak.com/channels/{channelID_R}/feeds.json?api_key={API_KEY_R}&results={model.Results}";
            var request_R = new RestRequest(url_R, Method.Get);
            var response_R = await client.ExecuteAsync(request_R);

            if (!response_A.IsSuccessful && !response_R.IsSuccessful)
            {
                return null;
            }

            var content_A = new ThingSpeakResponse();
            var content_R = new ThingSpeakResponse();

            try
            {
                content_A = JsonConvert.DeserializeObject<ThingSpeakResponse>(response_A.Content);
                content_R = JsonConvert.DeserializeObject<ThingSpeakResponse>(response_R.Content);
            }
            catch (ArgumentOutOfRangeException e)
            {

            }

            var viewModel = new SensorModel();
            viewModel.Ard_Created_At = content_A.Feeds.Select(p => p.Created_At).ToList();
            viewModel.PM1 = content_A.Feeds.Select(p => p.Field1).ToList();
            viewModel.PM25 = content_A.Feeds.Select(p => p.Field2).ToList();
            viewModel.PM10 = content_A.Feeds.Select(p => p.Field3).ToList();
            viewModel.CO = content_A.Feeds.Select(p => p.Field4).ToList();
            viewModel.Benzene = content_A.Feeds.Select(p => p.Field5).ToList();

            viewModel.Raspb_Created_At = content_R.Feeds.Select(p => p.Created_At).ToList();
            viewModel.Temperature = content_R.Feeds.Select(p => p.Field6).ToList();
            viewModel.Humidity = content_R.Feeds.Select(p => p.Field7).ToList();
            viewModel.Pressure = content_R.Feeds.Select(p => p.Field8).ToList();

            viewModel.Results = model.Results;
            viewModel.Location = model.Location;

            return (viewModel);
        }
    }
}
