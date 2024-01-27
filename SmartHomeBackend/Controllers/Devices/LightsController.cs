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
    [Route("api/system/{systemId}/board/{boardId}/devices/lights")]
    [ApiController]
    public class LightsController : ControllerBase
    {
        private readonly SmartHomeDbContext _context;
        private readonly DeviceService _deviceService;

        public LightsController(SmartHomeDbContext context, DeviceService deviceService)
        {
            _context = context;
            _deviceService = deviceService;
        }

        [Route("states")]
        [HttpGet]
        public async Task<IActionResult> GetAllLightsStates()
        {
            string url = $"{Strings.RPI_API_URL_ADRIAN}/lights/states";
            try
            {
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
                    
                    return Ok(_context.SwitchableLights);
                } else
                {
                    throw new Exception("Couldn't get all lights states.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("states/{lightId}")]
        [HttpGet]
        public async Task<IActionResult> GetOneLightState(int lightId)
        {
            string url = $"{Strings.RPI_API_URL_ADRIAN}/lights/states";
            try
            {
                if (_context.SwitchableLights.Find(lightId.ToString()) == null)
                    throw new Exception($"Light with id {lightId} not found in database.");

                var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

                if (response.IsSuccessStatusCode)
                {
                    var text = jsonDocument.RootElement.GetRawText();
                    var array = JsonSerializer.Deserialize<SwitchableLightDto[]>(text);
                    var light = array.Where(l => l.lightId == lightId).FirstOrDefault();
                    var lightInDB = _context.SwitchableLights.Find(lightId.ToString());
                    lightInDB.Value = light.isOn;

                    _context.SaveChanges();
                    
                    return Ok(_context.SwitchableLights.Find(lightId.ToString()));
                } else
                {
                    throw new Exception("Couldn't get a light state.");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("states")]
        [HttpPut]
        public async Task<IActionResult> SetLightsStates([FromBody] SwitchableLightDto[] lightsStates)
        {
            try {
                foreach (var lightState in lightsStates)
                {
                    string url = $"{Strings.RPI_API_URL_ADRIAN}/lights/set/{lightState.lightId}/{lightState.isOn}";
                    
                    if (_context.SwitchableLights.Find(lightState.lightId.ToString()) == null)
                        throw new Exception($"Light with id {lightState.lightId} not found in database.");

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
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
