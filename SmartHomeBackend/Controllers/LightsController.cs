using Microsoft.AspNetCore.Mvc;

namespace SmartHomeBackend.Controllers
{
    [Route("api/system/{systemId}/board/{boardId}/devices/lights")]
    [ApiController]
    public class LightsController : ControllerBase
    {
        [Route("{lightId}")]
        [HttpGet]
        public async Task<IActionResult> ToggleLightState(int systemId, int boardId, int lightId)
        {
            string url = $"http://127.0.0.1:5000/api/system/{systemId}/board/{boardId}/devices/lights/{lightId}";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        return Ok(response);
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
