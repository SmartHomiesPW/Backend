using Microsoft.AspNetCore.Mvc;
using SmartHomeBackend.Database;
using System.Security.Policy;
using System.Text.Json;

namespace SmartHomeBackend.Services
{
    public class SwitchableLightService
    {
        private readonly SmartHomeDbContext _context;

        public SwitchableLightService(SmartHomeDbContext context)
        {
            _context = context;
        }

        public bool LightExistsInSystem(string boardId, string lightId)
        {
            return _context.SwitchableLights.Any(s => s.System_Id == boardId && s.Switchable_Light_Id == lightId);
        }

        public async Task<(HttpResponseMessage, JsonDocument)> SetLightValue(HttpClient client, string url, string boardId, string lightId, int value)
        {
            var data = new
            {
                Value = value.ToString()
            };

            string jsonData = JsonSerializer.Serialize(data);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PatchAsync(url, content);
            string jsonResponse = await response.Content.ReadAsStringAsync();
            var jsonDocument = JsonDocument.Parse(jsonResponse);

            if (response.IsSuccessStatusCode)
            {
                var light = _context.SwitchableLights.Where(s => s.System_Id == boardId && s.Switchable_Light_Id == lightId).First();
                light.Value = value;
                _context.SaveChanges();
            }

            return (response, jsonDocument);
        }

        public int GetLightValue(string boardId, string lightId)
        {
            var light = _context.SwitchableLights.Where(s => s.System_Id == boardId && s.Switchable_Light_Id == lightId).First();
            if (light != null)
            {
                return light.Value;
            } else
            {
                throw new InvalidDataException("Couldn't find the light");
            }
        }
    }
}
