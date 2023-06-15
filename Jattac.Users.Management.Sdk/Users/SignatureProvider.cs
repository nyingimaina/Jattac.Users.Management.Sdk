using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rocket.Libraries.Auth;

namespace Jattac.Users.Management.Sdk.Users
{
    internal class ExceptionSecretProvider : IRocketJwtSecretProvider
    {
        public Task<string> GetSecretAsync()
        {
            throw new Exception("No secret provider has been configured. Please configure a secret provider.");
        }
    }
}