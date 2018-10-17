using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Weather
{
    [Route("api/[controller]")]
    public class WeatherController : Controller
    {
        private readonly IWeatherProvider weatherProvider;

        public WeatherController(IWeatherProvider weatherProvider)
        {
            this.weatherProvider = weatherProvider;
        }

        [HttpGet("[action]")]
        public ActionResult<int> GetTempForZip([FromQuery(Name = "zip")] int zip)
        {
            if (zip == 0)
            {
                return BadRequest($"{nameof(zip)} cannot be 0");
            }

            var result = 60;
            return Ok(result);
        }
    }
}
