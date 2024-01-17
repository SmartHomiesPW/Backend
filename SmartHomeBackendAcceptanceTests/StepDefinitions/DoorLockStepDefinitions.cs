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
    public class DoorLockStepDefinitions
    {
        private RestClient _restClient;
        private RestClientOptions _restClientOptions;

        public DoorLockStepDefinitions()
        {
            var doorLocksUri = "http://ec2-16-16-180-91.eu-north-1.compute.amazonaws.com/api/system/1/board/1/devices/door-locks";
            _restClientOptions = new RestClientOptions(doorLocksUri)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true,
                ThrowOnAnyError = false,
                MaxTimeout = 5000,
            };
            _restClient = new RestClient(_restClientOptions);
        }

        [Given(@"Certain door locks in system")]
        public async void GivenCertainDoorLocksInTheSystem()
        {
            var getDoorLocks = $"states";
            var request1 = new RestRequest(getDoorLocks);
            var response1 = await _restClient.ExecuteGetAsync<List<DoorLock>>(request1);
            var doorLocks = response1.Data ?? new List<DoorLock>();
            doorLocks.Count.Should().Be(1);
            response1.IsSuccessStatusCode.Should().BeTrue();
        }

        [Then(@"Successfully turn on and off the door locks")]
        public async void ThenSuccessfullyTurnOnAndOffTheDoorLocks()
        {
            var putDoorLocks1 = $"set/1";
            var request1 = new RestRequest(putDoorLocks1);
            var putResponse1 = await _restClient.ExecutePutAsync(request1);
            putResponse1.IsSuccessful.Should().Be(true);

            var putDoorLocks2 = $"set/0";
            var request2 = new RestRequest(putDoorLocks2);
            var putResponse2 = await _restClient.ExecutePutAsync(request2);
            putResponse2.IsSuccessful.Should().Be(true);
        }
    }
}
