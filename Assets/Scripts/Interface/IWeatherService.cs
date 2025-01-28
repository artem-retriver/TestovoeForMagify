using System.Threading;
using System.Threading.Tasks;
using WeatherIntegration;

namespace Interface
{
    public interface IWeatherService
    {
        Task<WeatherData> GetWeatherDataAsync(double lat, double lon, float timeout, CancellationToken cancellationToken);
    }
}