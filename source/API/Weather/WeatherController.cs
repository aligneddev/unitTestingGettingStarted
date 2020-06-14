using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        public async Task<ActionResult<double>> CurrentTemp([FromQuery(Name = "zipcode")] int zipCode)
        {
            if (zipCode == 0)
            {
                return BadRequest($"{nameof(zipCode)} cannot be 0");
            }

            var result = await weatherHttpClient.GetCurrentTempAsync(zipCode);
            return Ok(result);
        }

        //[HttpGet("[action]")]
        //public async Task<ActionResult<WeatherForecastResponse>> PastWeather([FromQuery] int zipCode, [FromQuery] string dateTime)
        //{
        //    if(zipCode == 0)
        //    {
        //        return BadRequest($"{nameof(zipCode)} cannot be 0");
        //    }

        //    if (string.IsNullOrWhiteSpace(dateTime) || !DateTime.TryParse(dateTime, out var parsedDateTime))
        //    {
        //        return BadRequest($"{nameof(dateTime)} must be a valid date");
        //    }

        //    var weather = await weatherHttpClient.GetPastWeatherAsync(zipCode, parsedDateTime);
        //    return weather;
        //}

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<HistoricalData>>> HistoricalData([FromQuery] int zipCode, [FromQuery] string dateTime)
        {
            var data = new List<HistoricalData>
            {
                new HistoricalData
                {
                    DateTime = DateTime.Now.AddYears(-12),
                    ZipCode = "57105",
                    TempF = 30.0
                },
                new HistoricalData
                {
                    DateTime = DateTime.Now.AddYears(-1),
                    ZipCode = "57105",
                    TempF = 15.0
                },
                new HistoricalData
                {
                    DateTime = DateTime.Now.AddYears(-1),
                    ZipCode = "57105",
                    TempF = 20.0
                },
                new HistoricalData
                {
                    DateTime = DateTime.Now,
                    ZipCode = "57105",
                    TempF = -15.0
                }
            };

            return await Task.FromResult(Ok(data));
        }
    }
}
