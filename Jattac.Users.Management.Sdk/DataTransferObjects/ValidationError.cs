using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jattac.Users.Management.Sdk.DataTransferObjects
{
    internal class ValidationError
    {
        public List<string> Errors { get; set; } = new List<string>();
    }
}