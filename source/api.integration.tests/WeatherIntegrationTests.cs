using Api.Core.Weather;
using Api.Tests.Core;
using Api.Weather;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Api.Integration.Tests
{
    // I put this together in about an hour and a half following https://fullstackmark.com/post/20/painless-integration-testing-with-aspnet-core-web-api
    // I need to do some more researching, thinking and write up an article on when to use these tests vs unit tests (like the ones I have already)

    public class WeatherIntegrationTests : IClassFixture<BikeTrackingApiWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly BikeTrackingApiWebApplicationFactory<Startup> _factory;

        public WeatherIntegrationTests(BikeTrackingApiWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
            _factory = factory;
        }

        [Fact]
        public async Task CanGetHistoricalWeatherData()
        {
            // Act
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/api/weather/HistoricalData");

            // assert
            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task WeatherController_GetCurrentTemp_WithZipCode_ReturnsOkWithTemp()
        {
            // maybe you'd put all the unit and integration tests for GetCurrentTemp together
            // I'm not sure as you take an 800ms hit to setup the BikeTrackingApiWebApplicationFactory
            // Arrange
            var getWeatherHttpClient = new Mock<IGetWeatherHttpClient>();
            getWeatherHttpClient.Setup(g => g.GetCurrentTempAsync(It.IsAny<int>()))
                .ReturnsAsync(72.6);

            // Arrange
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddTransient<IGetWeatherHttpClient>((sp) => { return getWeatherHttpClient.Object; });
                });
            })
            .CreateClient();

            // Act
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/api/weather/CurrentTemp?zipCode=57105");

            // Assert
            httpResponse.EnsureSuccessStatusCode();
        }
    }
}
