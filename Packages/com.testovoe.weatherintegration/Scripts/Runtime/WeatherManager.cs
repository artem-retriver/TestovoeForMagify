using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Interface;
using UnityEngine;
using UnityEngine.Android;

namespace WeatherIntegration
{
    [Serializable]
    public class WeatherData
    {
        public string Source;
        public float Temperature;
        public float Humidity;
        public string Description;
    }
    
    public class WeatherManager : MonoBehaviour
    {
        private readonly List<IWeatherService> _services = new List<IWeatherService>();
    
        public void AddService(IWeatherService service)
        { 
            _services.Add(service);
        }
    
        public async Task<List<WeatherData>> GetWeather(double lat, double lon, float timeout, CancellationToken cancellationToken)
        { 
            List<WeatherData> results = new List<WeatherData>(); 
            
            foreach (var service in _services)
            { 
                try 
                { 
                    var data = await service.GetWeatherDataAsync(lat, lon, timeout, cancellationToken); 
                    results.Add(data);
                }
                catch (Exception ex)
                { 
                    Debug.LogError($"Error fetching weather data: {ex.Message}");
                }
            }
            return results;
        }
    
        public async Task<List<WeatherData>> GetWeather(float timeout, CancellationToken cancellationToken)
        {
            if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                Permission.RequestUserPermission(Permission.FineLocation);
            }
    
            if (!Input.location.isEnabledByUser)
            {
                throw new Exception("Location services are disabled.");
            }
            
            Input.location.Start();
    
            while (Input.location.status == LocationServiceStatus.Initializing && !cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(500, cancellationToken);
            }
    
            if (Input.location.status == LocationServiceStatus.Failed)
            {
                throw new Exception("Failed to get location.");
            }
    
            double lat = Input.location.lastData.latitude;
            double lon = Input.location.lastData.longitude;
    
            return await GetWeather(lat, lon, timeout, cancellationToken);
        }
    }
}