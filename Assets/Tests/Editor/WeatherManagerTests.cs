using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using WeatherIntegration;

public class WeatherManagerTests
{
    public async Task TestWeatherManager()
    {
        var weatherManager = new WeatherManager();
        weatherManager.AddService(new OpenMeteoService());
        weatherManager.AddService(new OpenWeatherMapService());
        var result = await weatherManager.GetWeather(40.7128, -74.0060, 5.0f, CancellationToken.None);
        Debug.Assert(result.Count == 2, "Weather data should contain 2 entries.");
    }
}
