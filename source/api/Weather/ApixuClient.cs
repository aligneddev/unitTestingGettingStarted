using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Api.Weather
{
    public interface IGetWeatherHttpClient
    {
        Task<double> GetCurrentTemp(int zipCode);
    }

    public class ApixuClient : IGetWeatherHttpClient
    {
        private readonly HttpClient _client;
        private static string key = "45d659c3bbb64b06ad4172120181710";

        public ApixuClient(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri($"https://api.apixu.com/v1/current.json?key={key}");
            httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json; charset=utf-8");
            _client = httpClient;
        }

        public async Task<double> GetCurrentTemp(int zipCode)
        {
            var response = await _client.GetStringAsync($"&q={zipCode}");
            var weather = ApiuxWeatherResponse.FromJson(response);
            return weather.Current.TempF;
        }
    }
}
