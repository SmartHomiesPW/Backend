using Microsoft.AspNetCore.Mvc;
using SmartHomeBackend.Controllers.Devices;
using SmartHomeBackend.Database;
using SmartHomeBackend.Globals;
using SmartHomeBackend.Models;
using System.Text.Json;

namespace SmartHomeBackend.Services
{
    /// <summary>
    /// Service responsible for operations associated with devices communication
    /// </summary>
    public class DeviceService
    {
        /// <returns>Information about all switchable lights in the database on success.</returns>
        public List<SwitchableLight> GetAllSwitchableLights(string boardId, SmartHomeDbContext context)
        {
            return context.SwitchableLights.Where(sl => sl.System_Id == boardId).ToList();
        }

        /// <returns>Information about all temperature sensors in the database on success.</returns>
        public List<TemperatureSensor> GetAllTemperatureSensors(string boardId, SmartHomeDbContext context)
        {
            return context.TemperatureSensors.Where(hs => hs.System_Id == boardId).ToList();
        }

        /// <returns>Information about all humidity sensors in the database on success.</returns>
        public List<HumiditySensor> GetAllHumiditySensors(string boardId, SmartHomeDbContext context)
        {
            return context.HumiditySensors.Where(hs => hs.System_Id == boardId).ToList();
        }

        /// <returns>Information about all sunlight sensors in the database on success.</returns>
        public List<SunlightSensor> GetAllSunlightSensors(string boardId, SmartHomeDbContext context)
        {
            return context.SunlightSensors.Where(hs => hs.System_Id == boardId).ToList();
        }

        /// <summary>Sends http post request to specified url.</summary>
        /// <returns>Response from the url, to which the request was sent on success.</returns>
        public async Task<(HttpResponseMessage, JsonDocument)> SendHttpPostRequest(string url, HttpContent content)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsync(url, content);
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var jsonDocument = JsonDocument.Parse(jsonResponse);

                return (response, jsonDocument);
            }
        }

        /// <summary>Sends http get request to specified url.</summary>
        /// <returns>Response from the url, to which the request was sent on success.</returns>
        public async Task<(HttpResponseMessage, JsonDocument)> SendHttpGetRequest(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var jsonDocument = JsonDocument.Parse(jsonResponse);

                return (response, jsonDocument);
            }
        }
    }
}
