using Jattac.Users.Management.Sdk.Configuration;
using Jattac.Users.Management.Sdk.DataTransferObjects;
using Rocket.Libraries.Auth;

namespace Jattac.Users.Management.Sdk.Users
{
    /// <summary>
    /// Authenticates users
    /// </summary>
    public class AuthenticationManager
    {

        /// <summary>
        /// Signs in a user asynchronously.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <param name="tenantId">The ID of the tenant.</param>
        /// <param name="onResponse">Optional callback function to handle the HTTP response.</param>
        /// <returns>A task representing the asynchronous sign-in operation.</returns>
        public async Task<SignResponse> SignInAsync(
            string username,
            string password,
            Guid tenantId,
            Action<HttpResponseMessage>? onResponse = null
        )
        {
            var apiCaller = new ApiCaller();
            var signResponse = await apiCaller.PostAsync<WrappedResponse<ValidationResponse<SignResponse>>>(
                new SignRequest
                {
                    Username = username,
                    Password = password,
                    Id = tenantId
                },
                relativeUrl: "api/v1/users/sign-in",
                tenantId: tenantId,
                onResponse: onResponse
            );

            if (signResponse.Payload.HasErrors)
            {
                throw new Exception($"Unable to sign in user '{signResponse.Payload.ValidationErrors.First().Errors.First()}'");
            }
            else if (signResponse.Payload.Entity == null)
            {
                throw new Exception("Unable to sign in user. No response received from server.");
            }

            return signResponse.Payload.Entity;
        }


        /// <summary>
        /// Checks if a token is valid.
        /// </summary>
        /// <param name="token">The token to validate.</param>
        /// <returns>True if the token is valid; otherwise, false.</returns>
        public async Task<bool> TokenIsValid(string token)
        {
            var rocketJwtTokenDecoder = new RocketJwtTokenDecoder(
                JattacUserManagementConfigurationManager.configurationSettings.RocketJwtSecretProvider
            );
            var decodedToken = await rocketJwtTokenDecoder.DecodeTokenAsync(token);
            return true;
        }

        /// <summary>
        /// Checks if a token has not expired.
        /// </summary>
        /// <param name="token">The token to check.</param>
        /// <returns>True if the token has not expired; otherwise, false.</returns>
        /// <remarks>
        /// IMPORTANT: This method does not check if the token is valid. It only checks if the token has not expired.
        /// To check if the token is valid, use the <see cref="TokenIsValid"/> method.
        /// </remarks>
        public bool TokenHasNotExpired(string token)
        {
            var tokenDescription = GetTokenDescription(token);
            return tokenDescription.IsExpired == false;
        }

        /// <summary>
        /// Gets the expiry date of a token. If the token has expired, the expiry date will be in the past.
        /// </summary>
        /// <param name="token">The token to check.</param>
        /// <returns>The expiry date of the token.</returns>
        public DateTimeOffset GetTokenExpiryDate(string token)
        {

            var tokenDescription = GetTokenDescription(token);
            return tokenDescription.Expires;
        }

        private TokenDescription GetTokenDescription(string token)
        {
            var rocketJwtTokenDecoder = new RocketJwtTokenDecoder(
                JattacUserManagementConfigurationManager.configurationSettings.RocketJwtSecretProvider
            );
            return rocketJwtTokenDecoder.GetTokenDescription(token);
        }
    }

}