using Api.Weather;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Tests.Weather
{
    [TestClass]
    [TestCategory(TestCategories.WeatherAPI)]
    public class WeatherControllerTests
    {
        private (WeatherController weatherController, Mock<IGetWeatherHttpClient> getWeatherHttpClient) Factory()
        {
            var getWeatherHttpClient = new Mock<IGetWeatherHttpClient>();
            //return (new WeatherController(getWeatherHttpClient.Object), getWeatherHttpClient);
            return (new WeatherController(), getWeatherHttpClient);
        }
    }
}
