using Api.Weather;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
            return (new WeatherController(getWeatherHttpClient.Object), getWeatherHttpClient);
        }

        [TestMethod]
        public async Task WeatherController_GetTemp_NoZipCode_Returns400()
        {
            // Arrange
            var (weatherController, getWeatherHttpClient) = Factory();

            // Act
            var result = await weatherController.GetTempForZipAsync(0);

            // Assert
            Assert.AreEqual(400, (result.Result as BadRequestObjectResult).StatusCode);
        }

        [TestMethod]
        public async Task WeatherController_GetTemp_ZipCode_CallsProviderWithZipCode()
        {
            /**
             * Given an API call
            * When asking for current temp
            * Then it calls the weather Api with the correct zip code 
            */
            // Arrange
            var zipCode = 57105;
            var (weatherController, getWeatherHttpClient) = Factory();

            // fake the return from weather provider with MOQ
            var fakeTemp = 72.6;
            getWeatherHttpClient.Setup(wp => wp.GetCurrentTemp(zipCode))
                .ReturnsAsync(fakeTemp);

            // Act
            var response = await weatherController.GetTempForZipAsync(zipCode);

            // Assert
            Assert.AreEqual(fakeTemp, (response.Result as OkObjectResult).Value);
        }
    }
}
