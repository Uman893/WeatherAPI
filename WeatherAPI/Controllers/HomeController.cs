using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WeatherAPI.Models;
using WeatherAPI.Models.ViewModels;
using WeatherAPI.Services;

namespace WeatherAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWeatherForcastService _weaterForcastService;

        public HomeController(ILogger<HomeController> logger, IWeatherForcastService weaterForcastService)
        {
            _logger = logger;
            _weaterForcastService = weaterForcastService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetWeather(string City)
        {
            var weatherApi = await _weaterForcastService.GetWeather(City);
            DisplayWeatherVM result = new DisplayWeatherVM();
            result.Country = weatherApi.sys.country;
            result.City = weatherApi.name;
            result.Description = weatherApi.weather[0].description;
            result.Humidity = Convert.ToString(weatherApi.main.humidity);
            result.TempFeelsLike = Convert.ToString(weatherApi.main.feels_like);
            result.Temp = Convert.ToString(weatherApi.main.temp);
            result.WeatherIcon = weatherApi.weather[0].icon;
            return Json(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}