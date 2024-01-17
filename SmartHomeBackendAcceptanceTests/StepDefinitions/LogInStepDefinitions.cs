using RestSharp;
using System.Security.Policy;
using System.Text.Json;
using SmartHomeBackendAcceptanceTests;

using TechTalk.SpecFlow.Assist;
using System.Net.NetworkInformation;
using SmartHomeBackendAcceptanceTests.Models;
using SmartHomeBackendAcceptanceTests.Models.Dto;

namespace SmartHomeBackendAcceptanceTests.StepDefinitions
{
    [Binding]
    public class LogInStepDefinitions
    {
        private RestClient _restClient;
        private RestClientOptions _restClientOptions;
        private IEnumerable<UserLoginDto> userCredentials;

        public LogInStepDefinitions() {
            var authUri = "http://ec2-16-16-180-91.eu-north-1.compute.amazonaws.com/api/auth";

            _restClientOptions = new RestClientOptions(authUri)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true,
                ThrowOnAnyError = false,
                MaxTimeout = 5000,
            };
            _restClient = new RestClient(_restClientOptions);
        }

        [When(@"I put the following credentials")]
        public void WhenIPutTheFollowingCredentials(Table table)
        {
            userCredentials = table.CreateSet<UserLoginDto>();
            userCredentials.ElementAt(0).Should().BeEquivalentTo(new UserLoginDto() { Email = "elo@com.com", Password="123" });
        }

        [Then(@"I log in to system successfully")]
        public async void ThenILogInToSystemSuccessfully()
        {
            var postLogin = $"login";
            string body = $"{{ \"email\": \"{userCredentials.ElementAt(0).Email}\", \"password\": \"{userCredentials.ElementAt(0).Password}\" }}";
            var request = new RestRequest(postLogin).AddBody(body);
            var response = await _restClient.ExecutePostAsync<User>(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}
