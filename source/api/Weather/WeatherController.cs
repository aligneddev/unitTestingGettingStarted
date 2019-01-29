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
        public async Task<ActionResult<double>> CurrentTemp([FromQuery(Name = "zipcode")] int zipCode)
        {
            return Ok(zipCode);
            //if (zipCode == 0)
            //{
            //    return BadRequest($"{nameof(zipCode)} cannot be 0");
            //}

            //var result = await weatherHttpClient.GetCurrentTempAsync(zipCode);
            //return Ok(result);
        }

        //[HttpGet("[action]")]
        //public async Task<ActionResult<ApiuxWeatherForecastResponse>> PastWeather([FromQuery(Name = "zipcode")] int zipCode, [FromQuery(Name = "dateTime")] string dateTime)
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

    }
}
