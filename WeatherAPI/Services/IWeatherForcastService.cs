using WeatherAPI.Models;

namespace WeatherAPI.Services
{
    public interface IWeatherForcastService
    {
        Task<WeatherApi> GetWeather(string city);
    }
}
