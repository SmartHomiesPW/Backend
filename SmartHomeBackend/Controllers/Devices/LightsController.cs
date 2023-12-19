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
            // rpi url = database.extractUrlBasedOnSystemIdAndBoardId

            string url = $"{Strings.RPI_API_URL}/lights/states";
            var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

            if (response.IsSuccessStatusCode)
            {
                return Ok(jsonDocument);
            }
            else
            {
                return StatusCode(int.Parse(response.StatusCode.ToString()), $"An error occurred: {response.Content}");
            }
        }

        [Route("states/{lightId}")]
        [HttpGet]
        public async Task<IActionResult> GetOneLightState(int lightId)
        {
            // rpi url = database.extractUrlBasedOnSystemIdAndBoardId

            string url = $"{Strings.RPI_API_URL}/lights/states";
            var (response, jsonDocument) = await _deviceService.SendHttpGetRequest(url);

            if (response.IsSuccessStatusCode)
            {
                var text = jsonDocument.RootElement.GetRawText();
                var array = JsonSerializer.Deserialize<SwitchableLightDto[]>(text);
                var light = array.Where(l => l.lightId == lightId).FirstOrDefault();

                return Ok(light);
            }
            else
            {
                return StatusCode(int.Parse(response.StatusCode.ToString()), $"An error occurred: {response.Content}");
            }
        }

        [Route("states")]
        [HttpPost]
        public async Task<IActionResult> SetLightsStates([FromBody] SwitchableLightDto[] lightsStates)
        {
            // rpi url = database.extractUrlBasedOnSystemIdAndBoardId
            string jsonData = JsonSerializer.Serialize(lightsStates);

            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            string url = $"{Strings.RPI_API_URL}/lights/states";
            var (response, jsonDocument) = await _deviceService.SendHttpPostRequest(url, content);

            if (response.IsSuccessStatusCode)
            {
                return Ok(jsonDocument);
            }
            else
            {
                return StatusCode(int.Parse(response.StatusCode.ToString()), $"An error occurred: {response.Content}");
            }
        }
    }
}
