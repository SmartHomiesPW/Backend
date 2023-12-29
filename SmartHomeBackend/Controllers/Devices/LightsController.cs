using Microsoft.AspNetCore.Mvc;
using SmartHomeBackend.Database;
using SmartHomeBackend.Models;
using SmartHomeBackend.Services;
using SmartHomeBackend.Globals;
using System.Text.Json;
using System.Text.Json.Nodes;
using SmartHomeBackend.Models.Dto;
using System.Text;

namespace SmartHomeBackend.Controllers.Devices
{
    [Route("api/system/1/board/1/devices/lights")]
    [ApiController]
    public class LightsController : ControllerBase
    {
        private readonly SmartHomeDbContext _context;
        private readonly SystemService _systemService;
        private readonly DeviceService _deviceService;

        public LightsController(SmartHomeDbContext context, SystemService systemService, DeviceService deviceService)
        {
            _context = context;
            _systemService = systemService;
            _deviceService = deviceService;
        }

        [Route("states")]
        [HttpGet]
        public async Task<IActionResult> GetAllLightsStates()
        {
            string url = $"{Strings.RPI_API_URL}/lights/states";
            var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

            if (response.IsSuccessStatusCode)
            {
                var text = jsonDocument.RootElement.GetRawText();
                var array = JsonSerializer.Deserialize<SwitchableLightDto[]>(text);
                foreach (var light in array)
                {
                    var lightInDB = _context.SwitchableLights.Find(light.lightId.ToString());
                    lightInDB.Value = light.isOn;
                }
                _context.SaveChanges();
            }

            return Ok(_context.SwitchableLights);
        }

        [Route("states/{lightId}")]
        [HttpGet]
        public async Task<IActionResult> GetOneLightState(int lightId)
        {
            string url = $"{Strings.RPI_API_URL}/lights/states";
            var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

            if (response.IsSuccessStatusCode)
            {
                var text = jsonDocument.RootElement.GetRawText();
                var array = JsonSerializer.Deserialize<SwitchableLightDto[]>(text);
                var light = array.Where(l => l.lightId == lightId).FirstOrDefault();
                var lightInDB = _context.SwitchableLights.Find(lightId.ToString());
                lightInDB.Value = light.isOn;

                _context.SaveChanges();
            }

            return Ok(_context.SwitchableLights.Find(lightId.ToString()));
        }

        [Route("states")]
        [HttpPut]
        public async Task<IActionResult> SetLightsStates([FromBody] SwitchableLightDto[] lightsStates)
        {
            foreach (var lightState in lightsStates)
            {
                string url = $"{Strings.RPI_API_URL}/lights/set/{lightState.lightId}/{lightState.isOn}";
                var (response, _) = await _deviceService.SendHttpGetRequest(url);
                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode(int.Parse(response.StatusCode.ToString()), $"An error occurred: {response.Content}");
                } else
                {
                    var lightInDB = _context.SwitchableLights.Find(lightState.lightId.ToString());
                    lightInDB.Value = lightState.isOn;
                }
            }
            
            _context.SaveChanges();
            
            return Ok(_context.SwitchableLights);
        }
    }
}
