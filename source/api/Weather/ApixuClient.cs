using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Api.Weather
{
    public interface IGetWeatherHttpClient
    {
        //Task<double> GetCurrentTempAsync(int zipCode);
        //Task<ApiuxWeatherForecastResponse> GetPastWeatherAsync(int zip, DateTime dateTime);
    }

    public class ApixuClient : IGetWeatherHttpClient
    {
        public static string apiKey = "45d659c3bbb64b06ad4172120181710";
        private readonly HttpClient httpClient;

        public ApixuClient(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri($"https://api.apixu.com/v1/");
            httpClient.DefaultRequestHeaders
              .Accept
              .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.httpClient = httpClient;
        }
    }
}
