using Api.Exceptions;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Api.Weather
{
    public interface IGetWeatherHttpClient
    {
        Task<double> GetCurrentTempAsync(int zipCode);
        Task<ApiuxWeatherForecastResponse> GetPastWeatherAsync(int zip, DateTime dateTime);
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

        public async Task<double> GetCurrentTempAsync(int zipCode)
        {
            string response = string.Empty;
            try
            {
                response = await httpClient.GetStringAsync($"current.json?key={apiKey}&q={zipCode}");
            }
            catch (HttpRequestException)
            {
               // for ApixuClientTests_GetTemp_GivenAZipCode_NotFound_ThrowsException discussion
                  // TODO check the body of the exception
                  throw new NotFoundException($"{zipCode} didn't work");
            }

            var weather = ApiuxWeatherCurrentResponse.FromJson(response);
            return weather.Current.TempF;
        }

        public async Task<ApiuxWeatherForecastResponse> GetPastWeatherAsync(int zipCode, DateTime dateTime)
        {
            var response = await httpClient.GetStringAsync($"history.json?key={apiKey}&q={zipCode}&date={dateTime}");
            return ApiuxWeatherForecastResponse.FromJson(response);
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
