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
        //Task<WeatherForecastResponse> GetPastWeatherAsync(int zip, DateTime dateTime);
    }

    public class WeatherStackClient : IGetWeatherHttpClient
    {
        // NOTE: put this in Azure Key Vault, not in source!
        public static string apiKey = "2d3de5f3a586aa58eae7a5776ce352f1";
        private readonly HttpClient httpClient;

        public WeatherStackClient(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri($"http://api.weatherstack.com/");
            httpClient.DefaultRequestHeaders
              .Accept
              .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.httpClient = httpClient;
        }

        public async Task<double> GetCurrentTempAsync(int zipCode)
        {
            string response;
            try
            {
                response = await httpClient.GetStringAsync($"current?access_key={apiKey}&type=zipcode&units=f&query={zipCode}");
            }
            catch (HttpRequestException)
            {
                // for ApixuClientTests_GetTemp_GivenAZipCode_NotFound_ThrowsException discussion
                // TODO check the body of the exception
                throw new NotFoundException($"{zipCode} didn't work");
            }

            var weather = WeatherMainResponse.FromJson(response);
            return weather.Current.Temperature;
        }


        // http://api.weatherstack.com/historical?access_key={key}&query=New%20York&historical_date=2015-01-21&hourly=1
        // your current subscription plan does not support historical weather data. Please upgrade your account to use this feature."}}
        //public async Task<WeatherForecastResponse> GetPastWeatherAsync(int zipCode, DateTime dateTime)
        //{
        //    // our current subscription plan does not support historical weather data. Please upgrade your account to use this feature."}}
        //    // won't work
        //    // http://api.weatherstack.com/historical?access_key=&query=New%20York&historical_date=2015-01-21&hourly=1
        //    var response = await httpClient.GetStringAsync($"history.json?key={apiKey}&q={zipCode}&date={dateTime}");
        //    return WeatherForecastResponse.FromJson(response);
        //}

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
