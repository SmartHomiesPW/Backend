using Microsoft.AspNetCore.Mvc;
using SmartHomeBackend.Controllers.Devices;
using SmartHomeBackend.Database;
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


        public List<SwitchableLight> GetAllLights(string boardId, SmartHomeDbContext context)
        {

            return context.SwitchableLights.Where(sl => sl.System_Id == boardId).ToList();

        }

        public async Task<(HttpResponseMessage, JsonDocument)> SendHttpRequest(string url)
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
