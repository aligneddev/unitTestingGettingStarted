using Api.Weather;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using RichardSzalay.MockHttp;
using System;
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
    }
}
