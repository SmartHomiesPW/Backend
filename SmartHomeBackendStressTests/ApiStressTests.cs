using SmartHomeBackend.Services;
using System.Diagnostics;
using Xunit;

namespace SmartHomeBackendStressTests
{
    public class ApiStressTests
    {
        private const string BaseUrl = "http://127.0.0.1:5000/api/system/1/board/1/devices/lights/1";
        private const int NumberOfRequests = 100;

        [Fact]
        public async Task TestApiStress()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            DeviceService ds = new DeviceService();
            
            var tasks = new Task[NumberOfRequests];
            for (var i = 0; i < NumberOfRequests; i++)
            {
                tasks[i] = ds.SendHttpGetRequest(BaseUrl);
            }
            await Task.WhenAll(tasks);

            stopwatch.Stop();
            Assert.True(stopwatch.ElapsedMilliseconds <= 1000);
            
        }
    }
}