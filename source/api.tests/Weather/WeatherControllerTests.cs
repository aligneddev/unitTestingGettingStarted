using Api.Weather;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Api.Tests.Weather
{
    [TestClass]
    [TestCategory(TestCategories.WeatherAPI)]
    public class WeatherControllerTests
    {
        private WeatherController Factory() {
            return new WeatherController();
        }
    }
}
