using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jattac.Users.Management.Sdk.Users;
using Rocket.Libraries.Auth;

namespace Jattac.Users.Management.Sdk.Configuration
{
    public class ConfigurationSettings
    {
        public string BaseUrl { get; set; } = string.Empty;

        public IRocketJwtSecretProvider RocketJwtSecretProvider { get; set; } = new ExceptionSecretProvider();
    }
}