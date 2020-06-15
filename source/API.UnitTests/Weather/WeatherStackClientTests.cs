using Api.Exceptions;
using Api.Tests.Core;
using Api.Weather;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RichardSzalay.MockHttp;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Api.UnitTests.Weather
{
    [TestClass]
    [TestCategory(TestCategories.WeatherAPI)]
    public class WeatherStackClientTests
    {
        private (WeatherStackClient weatherStackClient, MockHttpMessageHandler mockHttp) Factory()
        {
            var fakeHttpClient = new Mock<HttpClient>();

            // using https://github.com/richardszalay/mockhttp
            var mockHttp = new MockHttpMessageHandler();
            var httpClient = mockHttp.ToHttpClient();
            var weatherStackClient = new WeatherStackClient(httpClient);
            return (weatherStackClient, mockHttp);
        }

        [TestMethod]
        public async Task GetTemp_GivenAZipCode_ReturnsTemp()
        {
            Assert.Fail();
            
            
            //// Arrange
            //var (weatherStackClient, mockHttp) = Factory();
            //const int zipCode = 57105;
            //const long fakeTemp = (long)70.5;
            //var response = new WeatherMainResponse
            //{
            //    Current = new WeatherCurrentResponse
            //    {
            //        Temperature = fakeTemp
            //    }
            //};

            //var requestUri = $"http://api.weatherstack.com/current?access_key={WeatherStackClient.apiKey}&type=zipcode&units=f&query={zipCode}";
            //mockHttp.When(requestUri)
            //        .Respond("application/json", Serialize.ToJson(response));

            //// Act
            //var result = await weatherStackClient.GetCurrentTempAsync(zipCode);

            //// Assert
            //Assert.AreEqual(fakeTemp, result);
        }

        [TestMethod]
        public async Task GetTemp_GivenAZipCode_UsesThatZipCode()
        {

            Assert.Fail();

            //// Arrange
            //var (weatherStackClient, mockHttp) = Factory();
            //const int zipCode = 57105;
            //const long fakeTemp = (long)70.5;
            //var response = new WeatherMainResponse
            //{
            //    Current = new WeatherCurrentResponse
            //    {
            //        Temperature = fakeTemp
            //    }
            //};

            //var requestUri = $"http://api.weatherstack.com/current?access_key={WeatherStackClient.apiKey}&type=zipcode&units=f&query={zipCode}";
            //var request = mockHttp.When(requestUri)
            //        .Respond("application/json", Serialize.ToJson(response));

            //// Act
            //await weatherStackClient.GetCurrentTempAsync(zipCode);

            //// Assert
            //Assert.AreEqual(1, mockHttp.GetMatchCount(request));
        }

        [TestMethod]
        public async Task GetTemp_GivenAZipCode_NotFound_ThrowsException()
        {

            Assert.Fail();
            //    // how do we handle error states outside our control?
            //    // Arrange
            //    var (weatherStackClient, mockHttp) = Factory();
            //    const int zipCode = 57105;
            //    const long fakeTemp = (long)70.5;
            //    var response = new WeatherMainResponse
            //    {
            //        Current = new WeatherCurrentResponse
            //        {
            //            Temperature = fakeTemp
            //        }
            //    };
            //    var requestUri = $"http://api.weatherstack.com/current?access_key={WeatherStackClient.apiKey}&type=zipcode&units=f&query={zipCode}";

            //    var request = mockHttp.When(requestUri)
            //            .Respond(HttpStatusCode.NotFound);

            //    // Act and Assert
            //    await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
            //    {
            //        await weatherStackClient.GetCurrentTempAsync(zipCode);
            //    });
            //}


            // TOD add tests for historical
        }
}
