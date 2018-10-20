using Api.Weather;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
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
        public async Task WeatherController_GetCurrentTemp_NoZipCode_Returns400()
        {
            // Arrange
            var (weatherController, getWeatherHttpClient) = Factory();

            // Act
            var result = await weatherController.GetCurrentTempForZipAsync(0);

            // Assert
            Assert.AreEqual(400, (result.Result as BadRequestObjectResult).StatusCode);
        }

        [TestMethod]
        public async Task WeatherController_GetCurrentTemp_ZipCode_CallsProviderWithZipCode()
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
            var response = await weatherController.GetCurrentTempForZipAsync(zipCode);

            // Assert
            Assert.AreEqual(fakeTemp, (response.Result as OkObjectResult).Value);
        }

        [TestMethod]
        public async Task WeatherController_GetTemp_NoZipCode_Returns400()
        {
            // Arrange
            var (weatherController, getWeatherHttpClient) = Factory();

            // Act
            var result = await weatherController.GetCurrentTempForZipAsync(0);

            // Assert
            Assert.AreEqual(400, (result.Result as BadRequestObjectResult).StatusCode);
        }

        [TestMethod]
        public async Task WeatherController_GetPastTemp_NoZipCode_Returns400()
        {
            // Arrange
            var (weatherController, getWeatherHttpClient) = Factory();

            // Act
            var result = await weatherController.GetPastTempForZipAsync(0, string.Empty);

            // Assert
            Assert.AreEqual(400, (result.Result as BadRequestObjectResult).StatusCode);
        }

        [TestMethod]
        public async Task WeatherController_GetPastTemp_NoDateTime_Returns400()
        {
            // Arrange
            var (weatherController, getWeatherHttpClient) = Factory();

            // Act
            var result = await weatherController.GetPastTempForZipAsync(59785, string.Empty);

            // Assert
            Assert.AreEqual(400, (result.Result as BadRequestObjectResult).StatusCode);
        }

        [TestMethod]
        public async Task WeatherController_GetPastTemp_ZipCodeAndDate_CallsProviderWithZipCodeAndDate()
        {
            /**
             * Given an API call
            * When asking for past temp
            * Then it calls the weather Api with the correct zip code and date time
            */
            // Arrange
            var zipCode = 57105;
            var date = DateTime.Now;
            var (weatherController, getWeatherHttpClient) = Factory();

            // fake the return from weather provider with MOQ
            var fakeTemp = 72.6;
            var response = new ApiuxWeatherCurrentResponse
            {
                Current = new Current
                {
                    TempF = fakeTemp
                }
            };
            getWeatherHttpClient.Setup(wp => wp.GetPastWeather(zipCode, date))
                .ReturnsAsync(response);

            // Act
            var response = await weatherController.GetPastTempForZipAsync(zipCode, date);

            // Assert
            Assert.AreEqual(fakeTemp, (response.Result as OkObjectResult).Value);
        }
    }
}
