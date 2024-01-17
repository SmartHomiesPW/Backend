using RestSharp;
using SmartHomeBackendAcceptanceTests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeBackendAcceptanceTests.StepDefinitions
{
    [Binding]
    public class GetSensorsDataStepDefinitions
    {
        private RestClient _restClient;
        private RestClientOptions _restClientOptions;
        private List<HumiditySensor> humiditySensors;
        private List<TemperatureSensor> temperatureSensors;
        private List<SunlightSensor> lightSensors;

        public GetSensorsDataStepDefinitions()
        {
            var sensorsUri = "http://ec2-16-16-180-91.eu-north-1.compute.amazonaws.com/api/system/1/board/1/devices/sensors";
            humiditySensors = new List<HumiditySensor>();
            temperatureSensors = new List<TemperatureSensor>();
            lightSensors = new List<SunlightSensor>();
            _restClientOptions = new RestClientOptions(sensorsUri)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true,
                ThrowOnAnyError = false,
                MaxTimeout = 5000,
            };
            _restClient = new RestClient(_restClientOptions);
        }

        [Given(@"Certain sensors in the system")]
        public async void GivenCertainSensorsInTheSystem()
        {
            var getHumiditySensors = $"humidity/states";
            var request1 = new RestRequest(getHumiditySensors);
            var response1 = await _restClient.ExecuteGetAsync<List<HumiditySensor>>(request1);
            humiditySensors = response1.Data ?? new List<HumiditySensor>();
            humiditySensors.Count.Should().Be(2);
            response1.IsSuccessStatusCode.Should().BeTrue();

            var getTemperatureSensors = $"temperature/states";
            var request2 = new RestRequest(getTemperatureSensors);
            var response2 = await _restClient.ExecuteGetAsync<List<TemperatureSensor>>(request2);
            temperatureSensors = response2.Data ?? new List<TemperatureSensor>();
            temperatureSensors.Count.Should().Be(2);
            response2.IsSuccessStatusCode.Should().BeTrue();

            var getLightSensors = $"sunlight/states";
            var request3 = new RestRequest(getLightSensors);
            var response3 = await _restClient.ExecuteGetAsync<List<SunlightSensor>>(request3);
            lightSensors = response3.Data ?? new List<SunlightSensor>();
            lightSensors.Count.Should().Be(1);
            response3.IsSuccessStatusCode.Should().BeTrue();

        }
    }
}
