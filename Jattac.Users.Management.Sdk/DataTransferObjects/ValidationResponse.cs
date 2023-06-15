using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jattac.Users.Management.Sdk.DataTransferObjects
{
    internal class ValidationResponse<TEntity>
    {
        public bool HasErrors { get; set; }

        public List<ValidationError> ValidationErrors { get; set; } = new List<ValidationError>();

        public TEntity? Entity { get; set; }
    }
}