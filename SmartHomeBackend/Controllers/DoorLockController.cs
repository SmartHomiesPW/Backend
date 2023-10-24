using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SmartHomeBackend.Controllers
{
    [Route("api/system/{systemId}/board/{boardId}/devices/door-lock")]
    [ApiController]
    public class DoorLockController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetDoorLockProperties(int systemId, int boardId)
        {
            string url = $"http://127.0.0.1:5000/api/system/{systemId}/board/{boardId}/devices/door-lock";
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

        [Route("log")]
        [HttpGet]
        public async Task<IActionResult> GetDoorLockState(int systemId, int boardId)
        {
            string url = $"http://127.0.0.1:5000/api/system/{systemId}/board/{boardId}/devices/door-lock/log";
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
