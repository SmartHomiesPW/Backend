using Microsoft.AspNetCore.Mvc;
using SmartHomeBackend.Controllers.Devices;
using SmartHomeBackend.Database;
using SmartHomeBackend.Globals;
using SmartHomeBackend.Models;
using System.Text.Json;

namespace SmartHomeBackend.Services
{
    public class DeviceService
    {
        public bool DeviceExistsInSystem(string boardId, string lightId, string deviceType, SmartHomeDbContext context)
        {
            switch (deviceType)
            {
                case "light":
                    return context.SwitchableLights.Any(s => s.System_Id == boardId && s.Switchable_Light_Id == lightId);
                default:
                    break;
            }

            return false;
        }


        public List<SwitchableLight> GetAllSwitchableLights(string boardId, SmartHomeDbContext context)
        {
            return context.SwitchableLights.Where(sl => sl.System_Id == boardId).ToList();
        }

        public List<TemperatureSensor> GetAllTemperatureSensors(string boardId, SmartHomeDbContext context)
        {
            return context.TemperatureSensors.Where(hs => hs.System_Id == boardId).ToList();
        }
        public List<HumiditySensor> GetAllHumiditySensors(string boardId, SmartHomeDbContext context)
        {
            return context.HumiditySensors.Where(hs => hs.System_Id == boardId).ToList();
        }

        public List<SunlightSensor> GetAllSunlightSensors(string boardId, SmartHomeDbContext context)
        {
            return context.SunlightSensors.Where(hs => hs.System_Id == boardId).ToList();
        }

        public async Task<(HttpResponseMessage, JsonDocument)> SendHttpPostRequest(string url, HttpContent content)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.PostAsync(url, content);
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var jsonDocument = JsonDocument.Parse(jsonResponse);

                    return (response, jsonDocument);
                }
                catch (HttpRequestException e)
                {
                    throw e;
                }
            }
        }

        public async Task<(HttpResponseMessage, JsonDocument)> SendHttpGetRequest(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var jsonDocument = JsonDocument.Parse(jsonResponse);

                    return (response, jsonDocument);
                }
                catch (HttpRequestException e)
                {
                    throw e;
                }
            }
        }
    }
}
