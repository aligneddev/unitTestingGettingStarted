using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Api.Weather
{
    public interface IGetWeatherHttpClient
    {
        Task<double> GetCurrentTemp(int zipCode);
        Task<ApiuxWeatherCurrentResponse> GetPastWeather(int zip, DateTime dateTime);
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

        public async Task<double> GetCurrentTemp(int zipCode)
        {
            var response = await httpClient.GetStringAsync($"current.json?key={apiKey}&q={zipCode}");
            var weather = ApiuxWeatherCurrentResponse.FromJson(response);
            return weather.Current.TempF;
        }

        public async Task<ApiuxWeatherCurrentResponse> GetPastWeather(int zipCode, DateTime dateTime)
        {
            var response = await httpClient.GetStringAsync($"past.json?key={apiKey}&q={zipCode}&date={dateTime}");
            return ApiuxWeatherCurrentResponse.FromJson(response);
        }

        //public async Task<ApiuxWeatherResponse> GetPastWeather_Untestable(int zipCode, DateTime dateTime)
        //{
        //    using(var httpClient = new HttpClient())
        //    {
        //        httpClient.BaseAddress = new Uri($"https://api.apixu.com/v1/");
        //        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        var response = await httpClient.GetStringAsync($"past.json?key={apiKey}&q={zipCode}&date={dateTime}");
        //        return ApiuxWeatherResponse.FromJson(response);
        //    }
        //}
    }
}
