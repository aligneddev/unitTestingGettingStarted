using System.Threading.Tasks;

namespace Api.Weather
{
    public class WeatherProvider : IWeatherProvider
    {
        public WeatherProvider()
        {
        }

        public Task<double> GetTempForZip(int zipCode)
        {
            // use https://www.apixu.com/
            // key 45d659c3bbb64b06ad4172120181710

            return Task.FromResult(89.8);
        }
    }
}
