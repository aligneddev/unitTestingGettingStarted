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
        public async Task<ActionResult<int>> GetTempForZipAsync([FromQuery(Name = "zip")] int zip)
        {
            if (zip == 0)
            {
                return BadRequest($"{nameof(zip)} cannot be 0");
            }

            var result = await weatherProvider.GetTempForZip(zip);
            return Ok(result);
        }
    }
}
