using StackForge.Domain.Shared.Errors;

namespace StackForge.Domain.Shared.Exceptions
{
    public class DomainException : Exception
    {
        public string Code { get; }

        public DomainException(DomainError error) : base(error.Message)
        {
            Code = error.Code;
        }
    }
}
