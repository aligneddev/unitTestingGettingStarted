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
            // an alternative to returning the tuple, is to have private properties on this class for the injected classes
            // this is useful if you're class/unit under test has too many dependencies
            // which means you should probably refactor that class to separate out concerns
            var getWeatherHttpClient = new Mock<IGetWeatherHttpClient>();
            return (new WeatherController(getWeatherHttpClient.Object), getWeatherHttpClient);
        }

        [TestMethod]
        public async Task WeatherController_GetCurrentTemp_NoZipCode_Returns400()
        {
            //Given an API call
            //When asking for current temp and no zip code is given
            //Then returns a 400
            Assert.Inconclusive();
        }

        //[TestMethod]
        //public async Task WeatherController_GetCurrentTemp_ZipCode_CallsWithZipCode()
        //{
        //    // Given an API call
        //    // When asking for current temp
        //    // Then it calls the weather Api with the correct zip code 
        //    Assert.Inconclusive();
        //}

        #region first tests
        /*

        [TestMethod]
        public async Task WeatherController_GetCurrentTemp_NoZipCode_Returns400()
        {
            // Arrange
            var (weatherController, getWeatherHttpClient) = Factory();

            // Act
            var result = await weatherController.CurrentTemp(0);

            // Assert
            Assert.AreEqual(400, (result.Result as BadRequestObjectResult).StatusCode);
        }        

        [TestMethod]
        public async Task WeatherController_GetCurrentTemp_NoOrBadZipCode_Returns400()
        {
            //Given an API call
            //When asking for current temp and no zip code is given
            //Then returns a 400
            // Arrange
            var (weatherController, getWeatherHttpClient) = Factory();

            // Act
            var result = await weatherController.CurrentTemp(0);

            // Assert
            Assert.AreEqual(400, (result.Result as BadRequestObjectResult).StatusCode);
        }
               
        // replaces WeatherController_GetCurrentTemp_NoOrBadZipCode_Returns400
        [TestMethod]
        [DataRow(0)]
        [DataRow(-57105)]
        [DataRow(12)]
        [DataRow(1921393)]
        public async Task WeatherController_GetCurrentTemp_NoOrBadZipCode_Returns400(int zipCode)
        {
            //Given an API call
            //When asking for current temp and no zip code is given
            //Then returns a 400
            // Arrange
            var (weatherController, getWeatherHttpClient) = Factory();

            // Act
            var result = await weatherController.CurrentTemp(zipCode);

            // Assert
            Assert.AreEqual(400, (result.Result as BadRequestObjectResult).StatusCode);
        }

        [TestMethod]
        public async Task WeatherController_GetCurrentTemp_ValidZipCode_CallsWithZipCode()
        {
            ///
             // Given an API call
             // When asking for current temp
             // Then it calls the weather Api with the correct zip code 
             ///
            // Arrange
            var zipCode = 57105;
            var (weatherController, getWeatherHttpClient) = Factory();

            // fake the return from weather provider with MOQ
            var fakeTemp = 72.6;
            getWeatherHttpClient.Setup(wp => wp.GetCurrentTempAsync(zipCode))
                .ReturnsAsync(fakeTemp);

            // Act
            var response = await weatherController.CurrentTemp(zipCode);

            // Assert
            getWeatherHttpClient.Verify(w => w.GetCurrentTempAsync(zipCode), Times.Once);

            // Note this test may not be the most useful, but it helps move us forward with TDD
            // some may just delete this test or skip writting it
        }       


        [TestMethod]
        public async Task WeatherController_GetCurrentTemp_ValidZipCode_ReturnsTheTemp()
        {
        // first make/leave WeatherController return zipcode in result
            // Arrange
            var zipCode = 57105;
            var (weatherController, getWeatherHttpClient) = Factory();
            var fakeTemp = 72.6;
            getWeatherHttpClient.Setup(wp => wp.GetCurrentTempAsync(zipCode))
               .ReturnsAsync(fakeTemp);

            // Act
            var result = await weatherController.CurrentTemp(zipCode);

            // Assert
            Assert.AreEqual(200, (result.Result as OkObjectResult).StatusCode);
            Assert.AreEqual(fakeTemp, (result.Result as OkObjectResult).Value);
        }
        */
        #endregion
        #region completed tests
        /*
        [TestMethod]
        public async Task WeatherController_GetTemp_NoZipCode_Returns400()
        {
            // Arrange
            var (weatherController, getWeatherHttpClient) = Factory();

            // Act
            var result = await weatherController.CurrentTemp(0);

            // Assert
            Assert.AreEqual(400, (result.Result as BadRequestObjectResult).StatusCode);
        }

        [TestMethod]
        public async Task WeatherController_GetPastTemp_NoZipCode_Returns400()
        {
            // Arrange
            var (weatherController, getWeatherHttpClient) = Factory();

            // Act
            var result = await weatherController.PastWeather(0, string.Empty);

            // Assert
            Assert.AreEqual(400, (result.Result as BadRequestObjectResult).StatusCode);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("00-1-2 10:00:00")]
        public async Task WeatherController_GetPastTemp_NoDateTimeOrInvalid_Returns400(string dateTime)
        {
            // Arrange
            var (weatherController, getWeatherHttpClient) = Factory();

            // Act
            var result = await weatherController.PastWeather(59785, string.Empty);

            // Assert
            Assert.AreEqual(400, (result.Result as BadRequestObjectResult).StatusCode);
        }

        [TestMethod]
        public async Task WeatherController_GetPastTemp_ZipCodeAndDate_CallsProviderWithZipCodeAndDate()
        {
            // 
             // Given an API call
             // When asking for past temp
             // Then it calls the weather Api with the correct zip code and date time
             // 
            // Arrange
            var zipCode = 57105;
            var date = DateTime.Now;
            var (weatherController, getWeatherHttpClient) = Factory();

            // fake the return from weather provider with MOQ
            var fakeTemp = 72.6;
            var fakeResponse = new ApiuxWeatherForecastResponse
            {
                Forecast = new ApiuxWeatherForecast
                {
                    ForecastDay = new List<ForecastDay> {
                        new ForecastDay
                        {
                            Hour = new List<ForecastHour> {
                               new ForecastHour
                               {
                                   FeelslikeF = fakeTemp
                               }
                            }
                        }
                    }
                }
            };
            getWeatherHttpClient.Setup(wp => wp.GetPastWeatherAsync(It.IsAny<int>(), It.IsAny<DateTime>()))
                //It.Is<DateTime>(d => d.ToString() == date.ToString())))
                .ReturnsAsync(fakeResponse);

            // Act
            var result = await weatherController.PastWeather(zipCode, date.ToString());

            // Assert
            getWeatherHttpClient.Verify(w => w.GetPastWeatherAsync(zipCode,
                // needs to use It.Is straight date doesn't match
                It.Is<DateTime>(d => d.ToString() == date.ToString())),
                Times.Once);

            // this really only tests the MOQ
            //var weatherResult = ApiuxWeatherCurrentResponse.FromJson((result.Result as OkObjectResult).Value as string);
            //Assert.AreEqual(fakeResponse.Current.TempF, weatherResult.Current.TempF);
        }

        */
        #endregion
        //// TODO add more tests and code if formatting the data response to abstract from Apiux is required
    }
}
