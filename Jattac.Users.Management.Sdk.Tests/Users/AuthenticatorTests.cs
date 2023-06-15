using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jattac.Users.Management.Sdk.Configuration;
using Jattac.Users.Management.Sdk.Users;

namespace Jattac.Users.Management.Sdk.Tests.Users
{
    public class AuthenticatorTests
    {
        [Fact(DisplayName = "Sign in integration test", Skip = "Integration test")]
        /// <summary>
        /// This test is an integration test and requires a running instance of the Jattac.Users.Management.Api project.
        /// It is skipped by default.
        /// To run it, remove the Skip attribute.
        /// Also ensure that the user with the following credentials exists:
        /// Username: test@example
        /// Password: test
        /// Tenant ID: 2dfabe0a-e370-11ed-92a5-0242ac120002
        /// The tenant ID is the ID of the tenant that the user belongs to.
        /// </summary>
        /// <returns></returns>
        public async Task SignIn_IntegrationTest()
        {
            JattacUserManagementConfigurationManager.Configure(new ConfigurationSettings
            {
                BaseUrl = "http://localhost:5141",
            });
            var authenticator = new Authenticator();
            var signInResponse = await authenticator.SignInAsync(
                "test@example.com",
                "test",
                new Guid("2dfabe0a-e370-11ed-92a5-0242ac120002")
            );
            Assert.NotEmpty(signInResponse.Token);
        }
    }
}