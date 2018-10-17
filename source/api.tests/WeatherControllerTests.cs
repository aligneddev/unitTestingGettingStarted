using Api.Weather;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Api.Tests
{
    [TestClass]
    [TestCategory(TestCategories.WeatherAPI)]
    public class WeatherControllerTests
    {

        private (WeatherController weatherController, Mock<IWeatherProvider> weatherProvider) Factory()
        {
            var weatherProvider = new Mock<IWeatherProvider>();
            return (new WeatherController(weatherProvider.Object), weatherProvider);
        }

        [TestMethod]
        public void WeatherController_GetTemp_NoZipCode_Returns400()
        {
            // Arrange
            var (weatherController, weatherProvider) = Factory();

            // Act
            var result = weatherController.GetTempForZip(0);

            // Assert
            Assert.AreEqual(400, (result.Result as BadRequestObjectResult).StatusCode);
        }

        /**
         * Given an API call
        * When asking for current temp
        * Then it calls the weather Api with the correct zip code 
        */
    }
}
