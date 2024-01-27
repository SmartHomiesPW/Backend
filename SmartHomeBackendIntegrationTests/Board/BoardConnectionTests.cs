using Xunit;
using SmartHomeBackend.Services;
using SmartHomeBackend.Globals;

namespace SmartHomeBackendIntegrationTests.Board
{
    public class BoardConnectionTests
    {
        [Fact]
        public async Task LightShouldBeSuccessfullySetOn()
        {
            DeviceService ds = new DeviceService();
            string url = $"{Strings.RPI_API_URL_MICHAL}/lights/set/1/1";
            var (response, _) = await ds.SendHttpGetRequest(url);

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task LightShouldBeSuccessfullySetOff()
        {
            DeviceService ds = new DeviceService();
            string url = $"{Strings.RPI_API_URL_MICHAL}/lights/set/1/0";
            var (response, _) = await ds.SendHttpGetRequest(url);

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task LightsStatesShouldBeSuccessfullyReceived()
        {
            DeviceService ds = new DeviceService();
            string url = $"{Strings.RPI_API_URL_MICHAL}/lights/states";
            var (response, _) = await ds.SendHttpGetRequest(url);

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task TemperatureSensorsShouldBeSuccessfullyReceived()
        {
            DeviceService ds = new DeviceService();
            string url = $"{Strings.RPI_API_URL_MICHAL}/sensors/temperature";
            var (response, _) = await ds.SendHttpGetRequest(url);

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task HumiditySensorsShouldBeSuccessfullyReceived()
        {
            DeviceService ds = new DeviceService();
            string url = $"{Strings.RPI_API_URL_MICHAL}/sensors/humidity";
            var (response, _) = await ds.SendHttpGetRequest(url);

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task SunlightSensorsShouldBeSuccessfullyReceived()
        {
            DeviceService ds = new DeviceService();
            string url = $"{Strings.RPI_API_URL_MICHAL}/sensors/light";
            var (response, _) = await ds.SendHttpGetRequest(url);

            Assert.True(response.IsSuccessStatusCode);
        }
    }
}