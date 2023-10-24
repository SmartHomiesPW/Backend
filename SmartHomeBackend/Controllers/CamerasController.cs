using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SmartHomeBackend.Controllers
{
    [Route("api/system/{systemId}/board/{boardId}/devices/cameras")]
    [ApiController]
    public class CamerasController : ControllerBase
    {
        [Route("{cameraId}")]
        [HttpGet]
        public async Task<IActionResult> GetCameraProperties(int systemId, int boardId, int cameraId)
        {
            string url = $"http://127.0.0.1:5000/api/system/{systemId}/board/{boardId}/devices/cameras/{cameraId}";
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

        [Route("{cameraId}/log")]
        [HttpGet]
        public async Task<IActionResult> GetCameraRecordings(int systemId, int boardId, int cameraId)
        {
            string url = $"http://127.0.0.1:5000/api/system/{systemId}/board/{boardId}/devices/cameras/{cameraId}/log";
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
