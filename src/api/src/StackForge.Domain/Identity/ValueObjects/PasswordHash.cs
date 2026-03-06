using StackForge.Domain.Identity.Errors;
using StackForge.Domain.Shared.Exceptions;
using StackForge.Domain.Shared.ValueObjects;

namespace StackForge.Domain.Identity.ValueObjects
{
    public sealed record PasswordHash : ValueObject
    {
        public string Value { get; }
        private PasswordHash(string value)
        {
            Value = value;
        }

        private PasswordHash() { }

        public PasswordHash Create(string value)
        {
            var normalizedValue = Normalize(value);
            Validate(normalizedValue);
            return new PasswordHash(normalizedValue);
        }

        private static string Normalize(string value) => value.Trim();

        private static void Validate(string value)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(value), UserError.PasswordRequired);
        }

    }
}
