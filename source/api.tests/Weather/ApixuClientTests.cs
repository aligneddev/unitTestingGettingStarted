using Api.Weather;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Api.Tests.Weather
{
    [TestClass]
    [TestCategory(TestCategories.WeatherAPI)]
    public class ApixuClientTests
    {
        private (ApixuClient apiuxClient, Mock<HttpClientHandler> mockClientHandler) Factory()
        {
            var fakeHttpClient = new Mock<HttpClient>();
            var mockClientHandler = new Mock<HttpClientHandler>();
            var httpClient = new HttpClient(mockClientHandler.Object);
            var apiuxClient = new ApixuClient(httpClient);
            return (apiuxClient, mockClientHandler);
        }

        [TestMethod]
        public async Task ApixuClientTests_GetTemp_GivenAZipCode_ReturnsTemp()
        {
            // Arrange
            var (apiuxClient, mockClientHandler) = Factory();
            const int zipCode = 57105;
            const double fakeTemp = 70.5;
            var response = new ApiuxWeatherResponse
            {
                Current = new Current
                {
                    TempF = fakeTemp
                }
            };
            mockClientHandler.SetupGetStringAsync(Serialize.ToJson(response));

            // Act
            var result = await apiuxClient.GetCurrentTemp(zipCode);

            // Assert
            Assert.AreEqual(fakeTemp, result);
        }
    }
}
