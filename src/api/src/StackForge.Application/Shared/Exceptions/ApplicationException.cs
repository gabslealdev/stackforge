using StackForge.Application.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackForge.Application.Shared.Exceptions
{
    public class ApplicationException: Exception
    {
        public ApplicationError Error { get; }

        public string Code => Error.Code;

        public ApplicationException(ApplicationError error) : base(error.Message)
        {
            Error = error;
        }
    }
}
