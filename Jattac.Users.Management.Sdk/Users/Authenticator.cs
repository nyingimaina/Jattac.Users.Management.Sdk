using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rocket.Libraries.FormValidationHelper;

namespace Jattac.Users.Management.Sdk.Users
{
    /// <summary>
    /// Authenticates users
    /// </summary>
    public class Authenticator
    {

        /// <summary>
        /// Signs in a user asynchronously.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <param name="tenantId">The ID of the tenant.</param>
        /// <param name="onResponse">Optional callback function to handle the HTTP response.</param>
        /// <returns>A task representing the asynchronous sign-in operation.</returns>
        public async Task<ValidationResponse<SignResponse>> SignInAsync(
            string username,
            string password,
            Guid tenantId,
            Action<HttpResponseMessage>? onResponse = null
        )
        {
            var apiCaller = new ApiCaller();
            var signResponse = await apiCaller.PostAsync<ValidationResponse<SignResponse>>(
                new SignRequest
                {
                    Username = username,
                    Password = password,
                    Id = tenantId
                },
                relativeUrl: "users/signin",
                tenantId: tenantId,
                onResponse: onResponse
            );
            return signResponse;
        }
    }

}