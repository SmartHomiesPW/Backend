using Microsoft.AspNetCore.Mvc;
using SmartHomeBackend.Database;
using SmartHomeBackend.Services;
using System.Text.Json;

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
        
        [Route("{lightId}")]
        [HttpGet]
        public async Task<IActionResult> ToggleLightState(int systemId, int boardId, int lightId)
        {
            if (!_systemService.SystemExists(boardId.ToString()))
            {
                throw new HttpRequestException("System does not exist.");
            }
            if (!_deviceService.DeviceExistsInSystem(boardId.ToString(), lightId.ToString(), "light", _context))
            {
                throw new HttpRequestException("Light does not exist in system.");
            }

            string url = $"http://127.0.0.1:5000/api/system/{systemId}/board/{boardId}/devices/lights/{lightId}";
            var (response, jsonDocument) = await _deviceService.SendHttpRequest(url);
            if (response.IsSuccessStatusCode)
            {
                return Ok(jsonDocument);
            }
            else
            {
                return StatusCode(int.Parse(response.StatusCode.ToString()), $"An error occurred: {response.Content}");
            }
        }

        [Route("{lightId}/log")]
        [HttpGet]
        public async Task<IActionResult> GetLightValue(int systemId, int boardId, int lightId)
        {
            string url = $"http://127.0.0.1:5000/api/system/{systemId}/board/{boardId}/devices/lights/{lightId}/log";
            var (response, jsonDocument) = await _deviceService.SendHttpRequest(url);
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
