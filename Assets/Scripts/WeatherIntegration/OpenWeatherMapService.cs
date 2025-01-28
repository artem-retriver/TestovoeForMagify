using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Interface;
using WeatherIntegration;

public class OpenWeatherMapService : IWeatherService
{
    private static readonly HttpClient _httpClient = new HttpClient();
    private string apiKey = "YOUR_API_KEY";

    public async Task<WeatherData> GetWeatherDataAsync(double lat, double lon, float timeout, CancellationToken cancellationToken)
    {
        string url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={apiKey}&units=metric";
        HttpResponseMessage response = await _httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
            
        return new WeatherData { Source = "OpenWeatherMap", Temperature = 22.0f, Humidity = 60.0f, Description = "Cloudy" };
    }
}