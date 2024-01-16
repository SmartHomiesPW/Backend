using SmartHomeBackend.Database;
using SmartHomeBackend.Globals;
using SmartHomeBackend.Models;
using SmartHomeBackend.Models.Dto;
using System.Linq;
using System.Text.Json;

namespace SmartHomeBackend.Services
{
    public class DatabaseRefreshService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DatabaseRefreshService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
                RefreshDatabase();
            }
        }

        private void RefreshDatabase()
        {
            RefreshLightsData();
            RefreshSensorsData();
        }

        private async void RefreshLightsData()
        {
            string url = $"{Strings.RPI_API_URL_MICHAL}/lights/states";
            using (var scope = _scopeFactory.CreateScope())
            {
                var _deviceService = scope.ServiceProvider.GetRequiredService<DeviceService>();
                var _context = scope.ServiceProvider.GetRequiredService<SmartHomeDbContext>();
                var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

                if (response.IsSuccessStatusCode)
                {
                    var lightsData = JsonSerializer.Deserialize<SwitchableLightDto[]>(jsonDocument);
                    foreach (var lightData in lightsData)
                    {
                        var light = _context.Set<SwitchableLight>().Find(lightData.lightId.ToString());
                        if (light != null)
                        {
                            light.Value = lightData.isOn;
                        }
                    }
                    _context.SaveChanges();
                }
            }
        }

        private void RefreshSensorsData()
        {
            RefreshTemperatureSensorsData();
            RefreshSunlightSensorsData();
            RefreshHumiditySensorsData();
        }

        private async void RefreshTemperatureSensorsData()
        {
            string url = $"{Strings.RPI_API_URL_MICHAL}/sensors/temperature";
            using (var scope = _scopeFactory.CreateScope())
            {
                var _deviceService = scope.ServiceProvider.GetRequiredService<DeviceService>();
                var _context = scope.ServiceProvider.GetRequiredService<SmartHomeDbContext>();
                var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

                if (response.IsSuccessStatusCode)
                {
                    var temperaturesData = JsonSerializer.Deserialize<TemperatureSensorMeasureDto[]>(jsonDocument);
                    foreach (var temperatureData in temperaturesData)
                    {
                        var sensor = _context.Set<TemperatureSensor>().Find(temperatureData.sensorId.ToString());
                        if (sensor != null)
                        {
                            sensor.Value = (decimal)temperatureData.temperature;
                        }
                    }
                    _context.SaveChanges();
                }
            }
        }

        private async void RefreshSunlightSensorsData()
        {
            string url = $"{Strings.RPI_API_URL_MICHAL}/sensors/light";
            using (var scope = _scopeFactory.CreateScope())
            {
                var _deviceService = scope.ServiceProvider.GetRequiredService<DeviceService>();
                var _context = scope.ServiceProvider.GetRequiredService<SmartHomeDbContext>();
                var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

                if (response.IsSuccessStatusCode)
                {
                    var sunlightsData = JsonSerializer.Deserialize<SunlightSensorMeasureDto[]>(jsonDocument);
                    foreach (var sunlightData in sunlightsData)
                    {
                        var sensor = _context.Set<TemperatureSensor>().Find(sunlightData.sensorId.ToString());
                        if (sensor != null)
                        {
                            sensor.Value = (decimal)sunlightData.lightValue;
                        }
                    }
                    _context.SaveChanges();
                }
            }
        }

        private async void RefreshHumiditySensorsData()
        {
            string url = $"{Strings.RPI_API_URL_MICHAL}/sensors/humidity";
            using (var scope = _scopeFactory.CreateScope())
            {
                var _deviceService = scope.ServiceProvider.GetRequiredService<DeviceService>();
                var _context = scope.ServiceProvider.GetRequiredService<SmartHomeDbContext>();
                var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

                if (response.IsSuccessStatusCode)
                {
                    var humiditiesData = JsonSerializer.Deserialize<HumiditySensorMeasureDto[]>(jsonDocument);
                    foreach (var humidityData in humiditiesData)
                    {
                        var sensor = _context.Set<TemperatureSensor>().Find(humidityData.sensorId.ToString());
                        if (sensor != null)
                        {
                            sensor.Value = (decimal)humidityData.humidity;
                        }
                    }
                    _context.SaveChanges();
                }
            }
        }
    }

}
