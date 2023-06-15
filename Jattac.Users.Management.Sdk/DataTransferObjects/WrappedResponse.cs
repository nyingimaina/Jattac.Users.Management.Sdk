using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jattac.Users.Management.Sdk.DataTransferObjects
{
    internal class WrappedResponse<TPayload>
    {
        public TPayload Payload { get; set; } = default!;
    }
}