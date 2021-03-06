﻿using Api.Exceptions;
using Api.Tests.Core;
using Api.Weather;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using RichardSzalay.MockHttp;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Api.Tests.Weather
{
    [TestClass]
    [TestCategory(TestCategories.WeatherAPI)]
    public class ApixuClientTests
    {
        private (ApixuClient apiuxClient, MockHttpMessageHandler mockHttp) Factory()
        {
            var fakeHttpClient = new Mock<HttpClient>();

            // using https://github.com/richardszalay/mockhttp
            var mockHttp = new MockHttpMessageHandler(); 
            var httpClient = mockHttp.ToHttpClient();
            var apiuxClient = new ApixuClient(httpClient);
            return (apiuxClient, mockHttp);
        }

        [TestMethod]
        public async Task ApixuClientTests_GetTemp_GivenAZipCode_ReturnsTemp()
        {
            // Arrange
            var (apiuxClient, mockHttp) = Factory();
            const int zipCode = 57105;
            const double fakeTemp = 70.5;
            var response = new ApiuxWeatherCurrentResponse
            {
                Current = new ApiuxWeatherCurrent
                {
                    TempF = fakeTemp
                }
            };
            var requestUri = $"https://api.apixu.com/v1/current.json?key={ApixuClient.apiKey}&q={zipCode}";
            mockHttp.When(requestUri)
                    .Respond("application/json", Serialize.ToJson(response));

            // Act
            var result = await apiuxClient.GetCurrentTempAsync(zipCode);

            // Assert
            Assert.AreEqual(fakeTemp, result);
        }

        [TestMethod]
        public async Task ApixuClientTests_GetTemp_GivenAZipCode_UsesThatZipCode()
        {
            // Arrange
            var (apiuxClient, mockHttp) = Factory();
            const int zipCode = 57105;
            const double fakeTemp = 70.5;
            var response = new ApiuxWeatherCurrentResponse
            {
                Current = new ApiuxWeatherCurrent
                {
                    TempF = fakeTemp
                }
            };
            var requestUri = $"https://api.apixu.com/v1/current.json?key={ApixuClient.apiKey}&q={zipCode}";
            var request = mockHttp.When(requestUri)
                    .Respond("application/json", Serialize.ToJson(response));

            
            // Act
            var result = await apiuxClient.GetCurrentTempAsync(zipCode);

            // Assert
            Assert.AreEqual(1, mockHttp.GetMatchCount(request));
        }

        [TestMethod]
        public async Task ApixuClientTests_GetTemp_GivenAZipCode_NotFound_ThrowsException()
        {
            // how do we handle error states outside our control?
            // Arrange
            var (apiuxClient, mockHttp) = Factory();
            const int zipCode = 57105;
            const double fakeTemp = 70.5;
            var response = new ApiuxWeatherCurrentResponse
            {
                Current = new ApiuxWeatherCurrent
                {
                    TempF = fakeTemp
                }
            };
            var requestUri = $"https://api.apixu.com/v1/current.json?key={ApixuClient.apiKey}&q={zipCode}";
            var request = mockHttp.When(requestUri)
                    .Respond(HttpStatusCode.NotFound);

            // Act and Assert
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
            {
                await apiuxClient.GetCurrentTempAsync(zipCode);
            });
        }
    }
}
