using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Interface;
using WeatherIntegration;

public class OpenMeteoService : IWeatherService
{
    private static readonly HttpClient _httpClient = new HttpClient();
        
    public async Task<WeatherData> GetWeatherDataAsync(double lat, double lon, float timeout, CancellationToken cancellationToken)
    {
        string url = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&current_weather=true";
        HttpResponseMessage response = await _httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
            
        return new WeatherData { Source = "OpenMeteo", Temperature = 20.0f, Humidity = 50.0f, Description = "Clear" };
    }
}