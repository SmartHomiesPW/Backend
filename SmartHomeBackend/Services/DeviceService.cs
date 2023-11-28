using Microsoft.AspNetCore.Mvc;
using SmartHomeBackend.Database;
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
                case "sunlight-sensor":
                    return false;
                case "humidity-sensor":
                    return false;
            }

            return false;
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
