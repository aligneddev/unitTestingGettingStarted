using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Tests.Weather
{
    /// <summary>
    /// Mock the http client from https://github.com/dotnet/corefx/issues/1624#issuecomment-100755941
    /// </summary>
    internal static class MockHttpClientHandlerExtensions
    {
        public static void SetupGetStringAsync(this Mock<HttpClientHandler> mockHandler, string response)
        {
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",                 
                    ItExpr.IsAny<HttpRequestMessage>(),
                    // if you have to be specific or multiple calls in the same method change message => message.RequestUri == requestUri), 
                    ItExpr.IsAny<CancellationToken>())
                .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(response) }));
        }
    }
}
