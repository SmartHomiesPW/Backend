using Microsoft.AspNetCore.Mvc;
using SmartHomeBackend.Database;
using SmartHomeBackend.Models.Dto;
using SmartHomeBackend.Services;
using System.Text.Json;

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
        public async Task<IActionResult> GetAllLightsStates(string systemId, string boardId)
        {
            try
            {
                var boardURL = BoardService.GetBoardURL(systemId, boardId, _context);
                if (boardURL == null)
                {
                    return StatusCode(400, "An error occured: System or Board not found");
                }

                string url = $"{boardURL}/lights/states";

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
            catch
            {
                return Ok(_context.SwitchableLights);
            }
        }

        [Route("states/{lightId}")]
        [HttpGet]
        public async Task<IActionResult> GetOneLightState(string systemId, string boardId, int lightId)
        {
            var boardURL = BoardService.GetBoardURL(systemId, boardId, _context);
            if (boardURL == null)
            {
                return StatusCode(400, "An error occured: System or Board not found");
            }

            string url = $"{boardURL}/lights/states";
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
        public async Task<IActionResult> SetLightsStates(
            string systemId,
            string boardId,
            [FromBody] SwitchableLightDto[] lightsStates
            )
        {
            foreach (var lightState in lightsStates)
            {
                var boardURL = BoardService.GetBoardURL(systemId, boardId, _context);
                if (boardURL == null)
                {
                    return StatusCode(400, "An error occured: System or Board not found");
                }

                string url = $"{boardURL}/lights/set/{lightState.lightId}/{lightState.isOn}";
                var (response, _) = await _deviceService.SendHttpGetRequest(url);
                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode(int.Parse(response.StatusCode.ToString()), $"An error occurred: {response.Content}");
                }
                else
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
