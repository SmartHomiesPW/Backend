using RestSharp;
using SmartHomeBackendAcceptanceTests.Models;
using SmartHomeBackendAcceptanceTests.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeBackendAcceptanceTests.StepDefinitions
{
    [Binding]
    public class SwitchLightsStepDefinitions
    {
        private RestClient _restClient;
        private RestClientOptions _restClientOptions;
        private List<SwitchableLight> lightsStates;

        public SwitchLightsStepDefinitions()
        {
            var switchableLightsUri = "http://ec2-16-16-180-91.eu-north-1.compute.amazonaws.com/api/system/1/board/1/devices/lights";
            _restClientOptions = new RestClientOptions(switchableLightsUri)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true,
                ThrowOnAnyError = false,
                MaxTimeout = 5000,
            };
            _restClient = new RestClient(_restClientOptions);
        }

        [Given(@"Certain lights in certain rooms of my house")]
        public async void GivenCertainLightsInCertainRoomsOfMyHouse()
        {
            var postLogin = $"states";
            var request = new RestRequest(postLogin);
            var response = await _restClient.ExecuteGetAsync<List<SwitchableLight>>(request);
            lightsStates = response.Data ?? new List<SwitchableLight>();
            lightsStates.Count.Should().Be(13);
        }

        [Then(@"Switching the lights by clicking on them")]
        public async void ThenSwitchingTheLightsByClickingOnThem()
        {
            string lightsStateChange = $"states";
            for(int i = 0; i < 13; i++)
            {
                string body = $"[ {{ \"lightId\": {i}, \"isOn\": 1 }} ]";
                var request2 = new RestRequest(lightsStateChange).AddJsonBody(body);
                var putResponse = await _restClient.ExecutePutAsync(request2);
                putResponse.IsSuccessful.Should().Be(true);
            }
            for (int i = 0; i < 13; i++)
            {
                string body = $"[ {{ \"lightId\": {i}, \"isOn\": 0 }} ]";
                var request3 = new RestRequest(lightsStateChange).AddJsonBody(body);
                var putResponse = await _restClient.ExecutePutAsync(request3);
                putResponse.IsSuccessful.Should().Be(true);
            }
        }
    }
}
