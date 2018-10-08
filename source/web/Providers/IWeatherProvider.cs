using System.Collections.Generic;
using web.Models;

namespace web.Providers
{
    public interface IWeatherProvider
    {
        List<WeatherForecast> GetForecasts();
    }
}
