using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Api.Weather
{
    public interface IGetWeatherHttpClient
    {
        Task<double> GetCurrentTemp(int zipCode);
    }

    public class ApixuClient : IGetWeatherHttpClient
    {
        public static string apiKey = "45d659c3bbb64b06ad4172120181710";
        private readonly HttpClient _client;

        public ApixuClient(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri($"https://api.apixu.com/v1/");
            httpClient.DefaultRequestHeaders
              .Accept
              .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client = httpClient;
        }

        public async Task<double> GetCurrentTemp(int zipCode)
        {
            var response = await _client.GetStringAsync($"current.json?key={apiKey}&q={zipCode}");
            var weather = ApiuxWeatherResponse.FromJson(response);
            return weather.Current.TempF;
        }
    }
}
