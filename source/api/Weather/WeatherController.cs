using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Api.Weather
{
    [Route("api/[controller]")]
    public class WeatherController : Controller
    {
        private readonly IGetWeatherHttpClient weatherHttpClient;

        public WeatherController(IGetWeatherHttpClient weatherHttpClient)
        {
            this.weatherHttpClient = weatherHttpClient;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<int>> GetCurrentTempForZipAsync([FromQuery(Name = "zip")] int zip)
        {
            if (zip == 0)
            {
                return BadRequest($"{nameof(zip)} cannot be 0");
            }

            var result = await weatherHttpClient.GetCurrentTemp(zip);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<int>> GetPastTempForZipAsync([FromQuery(Name = "zip")] int zip, [FromQuery(Name = "dateTime")] string dateTime)
        {
            if(zip == 0)
            {
                return BadRequest($"{nameof(zip)} cannot be 0");
            }

            if (string.IsNullOrWhiteSpace(dateTime) || !DateTime.TryParse(dateTime, out var parsedDateTime))
            {
                return BadRequest($"{nameof(dateTime)} must be a valid date");
            }

            var weather = await weatherHttpClient.GetPastWeather(zip, parsedDateTime);

            return weather
        }

    }
}
