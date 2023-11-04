using Microsoft.AspNetCore.Mvc;
using SmartHomeBackend.Services;
using System.Text.Json;

namespace SmartHomeBackend.Controllers
{
    [Route("api/system/{systemId}/board/{boardId}/devices/lights")]
    [ApiController]
    public class LightsController : ControllerBase
    {
        private readonly SystemService _systemService;
        private readonly SwitchableLightService _switchableLightService;

        public LightsController(SystemService systemService, SwitchableLightService switchableLightService)
        {
            _systemService = systemService;
            _switchableLightService = switchableLightService;
        }

        [Route("{lightId}")]
        [HttpGet]
        public async Task<IActionResult> ToggleLightState(int systemId, int boardId, int lightId)
        {
            if (!_systemService.SystemExists(boardId.ToString()))
            {
                throw new HttpRequestException("System does not exist.");
            }
            if (!_switchableLightService.LightExistsInSystem(boardId.ToString(), lightId.ToString()))
            {
                throw new HttpRequestException("Light does not exist in system.");
            }
            
            string url = $"http://127.0.0.1:5000/api/system/{systemId}/board/{boardId}/devices/lights/{lightId}";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var newValue = 1 - _switchableLightService.GetLightValue(boardId.ToString(), lightId.ToString());

                    (HttpResponseMessage response, JsonDocument jsonDocument) = await _switchableLightService
                        .SetLightValue(client, url, boardId.ToString(), lightId.ToString(), newValue);

                    if (response.IsSuccessStatusCode)
                    {
                        return Ok(jsonDocument);
                    }
                    else
                    {
                        return StatusCode(int.Parse(response.StatusCode.ToString()), $"An error occurred: {response.Content}");
                    }
                }
                catch (HttpRequestException e)
                {
                    return StatusCode(500, $"An error occurred: {e.Message}");
                }
            }
        }

        [Route("{lightId}/log")]
        [HttpGet]
        public async Task<IActionResult> GetLightValue(int systemId, int boardId, int lightId)
        {
            string url = $"http://127.0.0.1:5000/api/system/{systemId}/board/{boardId}/devices/lights/{lightId}/log";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var jsonDocument = JsonDocument.Parse(jsonResponse);

                    if (response.IsSuccessStatusCode)
                    {
                        return Ok(jsonDocument);
                    }
                    else
                    {
                        return StatusCode(int.Parse(response.StatusCode.ToString()), $"An error occurred: {response.Content}");
                    }
                }
                catch (HttpRequestException e)
                {
                    return StatusCode(500, $"An error occurred: {e.Message}");
                }
            }
        }
    }
}
