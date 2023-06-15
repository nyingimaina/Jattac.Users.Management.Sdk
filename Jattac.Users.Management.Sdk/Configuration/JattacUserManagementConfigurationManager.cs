using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jattac.Users.Management.Sdk.Configuration
{
    public static class JattacUserManagementConfigurationManager
    {
        internal static ConfigurationSettings configurationSettings = new ConfigurationSettings();

        public static void Configure(ConfigurationSettings settings)
        {
            configurationSettings = settings;
        }
    }
}