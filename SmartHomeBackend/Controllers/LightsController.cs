using Microsoft.AspNetCore.Mvc;
using SmartHomeBackend.Models;
using System;
using System.Net;
using System.Xml.Linq;

namespace SmartHomeBackend.Controllers
{
    [Route("api/system/1/board/1/devices/lights/1")]
    [ApiController]
    public class LightsController : ControllerBase
    {
        [HttpGet]
        public async Task ToggleTheLightState()
        {
            string url = "http://127.0.0.1:5000/api/system/1/board/1/devices/lights/1";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        // Handle the case where the request was not successful.
                        Console.WriteLine($"Request failed with status code: {response.StatusCode}");
                    }
                }
                catch (HttpRequestException e)
                {
                    // Handle exceptions that may occur during the request.
                    Console.WriteLine($"Request error: {e.Message}");
                }
            }
        }
    }
}
