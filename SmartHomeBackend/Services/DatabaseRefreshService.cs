using SmartHomeBackend.Database;
using SmartHomeBackend.Globals;
using SmartHomeBackend.Models;
using SmartHomeBackend.Models.Dto;
using System.Linq;
using System.Text.Json;

namespace SmartHomeBackend.Services
{
    /// <summary>
    /// Service responsible for operations associated with database refreshment.
    /// </summary>
    public class DatabaseRefreshService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DatabaseRefreshService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        /// <summary>Makes a loop for database refreshment.</summary>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
                await RefreshDatabase();
            }
        }

        /// <summary>Invokes methods responsible for database refreshment.</summary>
        private async Task RefreshDatabase()
        {
            await RefreshLightsData();
            await RefreshSensorsData();
        }

        /// <summary>Refreshes switchable lights data in the database.</summary>
        private async Task RefreshLightsData()
        {
            string url = $"{Strings.RPI_API_URL_MICHAL}/lights/states";
            using (var scope = _scopeFactory.CreateScope())
            {
                var _deviceService = scope.ServiceProvider.GetRequiredService<DeviceService>();
                var _context = scope.ServiceProvider.GetRequiredService<SmartHomeDbContext>();
                var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

                if (response.IsSuccessStatusCode)
                {
                    var lightsData = JsonSerializer.Deserialize<SwitchableLightDto[]>(jsonDocument) ??
                        throw new Exception("Couldn't deserialize response into SwitchableLightDto[].");
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

        /// <summary>Invokes methods responsible for refreshment of sensors data in the database.</summary>
        private async Task RefreshSensorsData()
        {
            await RefreshTemperatureSensorsData();
            await RefreshSunlightSensorsData();
            await RefreshHumiditySensorsData();
        }

        /// <summary>Refreshes temperature sensors data in the database.</summary>
        private async Task RefreshTemperatureSensorsData()
        {
            string url = $"{Strings.RPI_API_URL_MICHAL}/sensors/temperature";
            using (var scope = _scopeFactory.CreateScope())
            {
                var _deviceService = scope.ServiceProvider.GetRequiredService<DeviceService>();
                var _context = scope.ServiceProvider.GetRequiredService<SmartHomeDbContext>();
                var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

                if (response.IsSuccessStatusCode)
                {
                    var temperaturesData = JsonSerializer.Deserialize<TemperatureSensorMeasureDto[]>(jsonDocument) ??
                        throw new Exception("Couldn't deserialize response into TemperatureSensorMeasureDto[].");
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

        /// <summary>Refreshes sunlight sensors data in the database.</summary>
        private async Task RefreshSunlightSensorsData()
        {
            string url = $"{Strings.RPI_API_URL_MICHAL}/sensors/light";
            using (var scope = _scopeFactory.CreateScope())
            {
                var _deviceService = scope.ServiceProvider.GetRequiredService<DeviceService>();
                var _context = scope.ServiceProvider.GetRequiredService<SmartHomeDbContext>();
                var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

                if (response.IsSuccessStatusCode)
                {
                    var sunlightsData = JsonSerializer.Deserialize<SunlightSensorMeasureDto[]>(jsonDocument) ??
                        throw new Exception("Couldn't deserialize response into SunlightSensorMeasureDto[].");
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

        /// <summary>Refreshes temperature humidity data in the database.</summary>
        private async Task RefreshHumiditySensorsData()
        {
            string url = $"{Strings.RPI_API_URL_MICHAL}/sensors/humidity";
            using (var scope = _scopeFactory.CreateScope())
            {
                var _deviceService = scope.ServiceProvider.GetRequiredService<DeviceService>();
                var _context = scope.ServiceProvider.GetRequiredService<SmartHomeDbContext>();
                var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

                if (response.IsSuccessStatusCode)
                {
                    var humiditiesData = JsonSerializer.Deserialize<HumiditySensorMeasureDto[]>(jsonDocument) ??
                        throw new Exception("Couldn't deserialize response into HumiditySensorMeasureDto[].");
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
