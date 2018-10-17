using Api.Weather;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Api.Tests.Weather
{
    [TestClass]
    [TestCategory(TestCategories.WeatherAPI)]
    public class WeatherProviderTests
    {
        private (IWeatherProvider weatherProvider, Mock<IGetWeatherHttpClient> getWeatherHttpClient) Factory()
        {
            var getWeatherHttpClient = new Mock<IGetWeatherHttpClient>();
            return (new WeatherProvider(getWeatherHttpClient.Object), getWeatherHttpClient);
        }

        [TestMethod]
        public async Task WeatherProvider_GivenAZipCode_ReturnsTemp()
        {
            // Arrange
            var (weatherProvider, getWeatherHttpClient) = Factory();
            const int zipCode = 57105;
            const double fakeTemp = 70.5;
            getWeatherHttpClient.Setup(hc => hc.GetCurrentTemp(zipCode))
                .ReturnsAsync(fakeTemp);

            // Act
            var result = await weatherProvider.GetTempForZipAsync(zipCode);

            // Assert
            Assert.AreEqual(fakeTemp, result);
        }
    }
}
