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
        private readonly ILogger<DatabaseRefreshService> _logger;
        private readonly DeviceService _deviceService;
        private readonly SmartHomeDbContext _context;

        public DatabaseRefreshService(ILogger<DatabaseRefreshService> logger, DeviceService deviceService, SmartHomeDbContext context)
        {
            _logger = logger;
            _deviceService = deviceService;
            _context = context;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                RefreshDatabase();

                _logger.LogInformation("Refreshing database at: {time}", DateTimeOffset.Now);

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        private void RefreshDatabase()
        {
            RefreshLightsData();
            RefreshSensorsData();
        }

        private async void RefreshLightsData()
        {
            string url = $"{Strings.RPI_API_URL}/lights/states";
            var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

            if (response.IsSuccessStatusCode)
            {
                var lightsData = JsonSerializer.Deserialize<SwitchableLightDto[]>(jsonDocument);
                foreach(var lightData in lightsData)
                {
                    var light = _context.Set<SwitchableLight>().Find(lightData.LightId.ToString());
                    if (light != null)
                    {
                        light.Value = lightData.IsOn ? 1 : 0;
                    }
                }
                _context.SaveChanges();
            }
            else
            {
                throw new Exception();
            }
        }

        private void RefreshSensorsData()
        {
            RefreshTemperatureSensorsData();
            RefreshSunlightSensorsData();
            RefreshHumiditySensorsData();
        }

        private void RefreshTemperatureSensorsData()
        {

        }

        private void RefreshSunlightSensorsData()
        {

        }

        private void RefreshHumiditySensorsData()
        {

        }
    }

}
