using System.Threading.Tasks;

namespace Api.Weather
{
    public interface IWeatherProvider
    {
        Task<double> GetTempForZip(int zipCode);
    }
}
