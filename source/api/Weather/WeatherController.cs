using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Api.Weather
{
    [Route("api/[controller]")]
    public class WeatherController : Controller
    {
        private readonly IGetWeatherHttpClient getWeatherHttpClient;

        public WeatherController(IGetWeatherHttpClient getWeatherHttpClient)
        {
            this.getWeatherHttpClient = getWeatherHttpClient;
        }

    }
}
