using RestSharp;
using SmartHomeBackendAcceptanceTests.Models;
using SmartHomeBackendAcceptanceTests.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Assist;

namespace SmartHomeBackendAcceptanceTests.StepDefinitions
{
    [Binding]
    public class RegisterStepDefinitions
    {
        private RestClient _restClient;
        private RestClientOptions _restClientOptions;
        private IEnumerable<UserRegistrationDto> userCredentials;

        public RegisterStepDefinitions()
        {
            var authUri = "http://ec2-16-16-180-91.eu-north-1.compute.amazonaws.com/api/auth";

            _restClientOptions = new RestClientOptions(authUri)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true,
                ThrowOnAnyError = false,
                MaxTimeout = 5000,
            };
            _restClient = new RestClient(_restClientOptions);
        }

        [When(@"I put the following credentials to register")]
        public void WhenIPutTheFollowingCredentialsToRegister(Table table)
        {
            userCredentials = table.CreateSet<UserRegistrationDto>();
            userCredentials.ElementAt(0).Should().BeEquivalentTo(new UserRegistrationDto() { Email = "tomek@test.com", Password = "123", FirstName = "Tomasz", LastName="Olbrychski" });
        }

        [Then(@"I register to system successfully")]
        public async void ThenILogInToSystemSuccessfully()
        {
            var postRegister = $"register";
            string emailAndPasswordText = $"\"email\": \"{userCredentials.ElementAt(0).Email}\", \"password\": \"{userCredentials.ElementAt(0).Password}\"";
            string firstNameText = !string.IsNullOrEmpty(userCredentials.ElementAt(0).FirstName) ? $",\"firstName\": \"{userCredentials.ElementAt(0).FirstName}\"" : "";
            string lastNameText = !string.IsNullOrEmpty(userCredentials.ElementAt(0).LastName) ? $",\"lastName\": \"{userCredentials.ElementAt(0).LastName}\"" : "";
            string body = '{' + emailAndPasswordText + firstNameText + lastNameText + '}';
            var request = new RestRequest(postRegister).AddBody(body);
            var response = await _restClient.ExecutePostAsync<User>(request);


            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}
