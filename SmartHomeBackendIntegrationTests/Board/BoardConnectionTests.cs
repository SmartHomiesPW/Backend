using Xunit;
using SmartHomeBackend.Services;
using SmartHomeBackend.Globals;

namespace SmartHomeBackendIntegrationTests.Board
{
    public class BoardConnectionTests
    {
        [Fact]
        public async void LightShouldBeSuccessfullySetOn()
        {
            DeviceService ds = new DeviceService();
            string url = $"{Strings.RPI_API_URL}/lights/set/1/1";
            var (response, _) = await ds.SendHttpGetRequest(url);

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async void LightShouldBeSuccessfullySetOff()
        {
            DeviceService ds = new DeviceService();
            string url = $"{Strings.RPI_API_URL}/lights/set/1/0";
            var (response, _) = await ds.SendHttpGetRequest(url);

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async void LightsStatesShouldBeSuccessfullyReceived()
        {
            DeviceService ds = new DeviceService();
            string url = $"{Strings.RPI_API_URL}/lights/states";
            var (response, _) = await ds.SendHttpGetRequest(url);

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async void TemperatureSensorsShouldBeSuccessfullyReceived()
        {
            DeviceService ds = new DeviceService();
            string url = $"{Strings.RPI_API_URL}/sensors/temperature";
            var (response, _) = await ds.SendHttpGetRequest(url);

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async void HumiditySensorsShouldBeSuccessfullyReceived()
        {
            DeviceService ds = new DeviceService();
            string url = $"{Strings.RPI_API_URL}/sensors/humidity";
            var (response, _) = await ds.SendHttpGetRequest(url);

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async void SunlightSensorsShouldBeSuccessfullyReceived()
        {
            DeviceService ds = new DeviceService();
            string url = $"{Strings.RPI_API_URL}/sensors/light";
            var (response, _) = await ds.SendHttpGetRequest(url);

            Assert.True(response.IsSuccessStatusCode);
        }
    }
}