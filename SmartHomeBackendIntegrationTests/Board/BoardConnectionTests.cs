using Xunit;
using SmartHomeBackend.Services;

namespace SmartHomeBackendIntegrationTests.Board
{
    public class BoardConnectionTests
    {
        [Fact]
        public async void LightShouldBeSuccessfullyToggled()
        {
            DeviceService ds = new DeviceService();
            string url = $"http://127.0.0.1:5000/api/system/1/board/1/devices/lights/1";
            var (response, _) = await ds.SendHttpRequest(url);

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async void LightLogShouldBeSuccessfullyReceived()
        {
            DeviceService ds = new DeviceService();
            string url = $"http://127.0.0.1:5000/api/system/1/board/1/devices/lights/1/log";
            var (response, _) = await ds.SendHttpRequest(url);

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async void TemperaturePropertiesShouldBeSuccessfullyReceived()
        {
            DeviceService ds = new DeviceService();
            string url = $"http://127.0.0.1:5000/api/system/1/board/1/devices/sensors/temperature/1";
            var (response, _) = await ds.SendHttpRequest(url);

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async void TemperatureLogShouldBeSuccessfullyReceived()
        {
            DeviceService ds = new DeviceService();
            string url = $"http://127.0.0.1:5000/api/system/1/board/1/devices/sensors/temperature/1/log";
            var (response, _) = await ds.SendHttpRequest(url);

            Assert.True(response.IsSuccessStatusCode);
        }
    }
}