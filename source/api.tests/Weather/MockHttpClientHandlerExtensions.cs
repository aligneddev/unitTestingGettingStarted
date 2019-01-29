namespace Api.Tests.Weather
{
    // use https://github.com/richardszalay/mockhttp instead
    /// <summary>
    /// Mock the http client from https://github.com/dotnet/corefx/issues/1624#issuecomment-100755941
    /// </summary>
    //internal static class MockHttpClientHandlerExtensions
    //{
    //    public static void SetupGetStringAsync(this Mock<HttpClientHandler> mockHandler, string response, Uri requestUri)
    //    {
    // if you have to be specific or multiple calls in the same method change or want to verify it
    //mockHandler.Protected()
    //    .Setup<Task<HttpResponseMessage>>("SendAsync",
    //        ItExpr.Is<HttpRequestMessage>(message => message.RequestUri == requestUri),
    //        ItExpr.IsAny<CancellationToken>())
    //    .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(response) }))
    //    .Verifiable();
    //    }
    //}
}
