using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jattac.Users.Management.Sdk.Users
{
    public class SignResponse
    {
        public string Token { get; set; } = string.Empty;

        public Guid UserId { get; set; }
    }
}