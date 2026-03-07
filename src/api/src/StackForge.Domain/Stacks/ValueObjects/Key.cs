using StackForge.Domain.Shared.Exceptions;
using StackForge.Domain.Shared.ValueObjects;
using StackForge.Domain.Stacks.Errors;

namespace StackForge.Domain.Stacks.ValueObjects
{
    public sealed record Key : ValueObject
    {
        private const int MaxLength = 20;

        public string Value { get; private set; }

        private Key(string value)
        {
            Value = value;
        }

        private Key() { }

        public static Key Create(string value)
        {
            var normalizedValue = Normalize(value);
            Validate(normalizedValue);

            return new Key(normalizedValue);
        }

        public override string ToString() => Value;

        private static void Validate(string value)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(value), StackErrors.StackKeyRequired);
            DomainExceptionValidation.When(value.Length > MaxLength, StackErrors.StackKeyTooLong);
        }

        private static string Normalize(string value)
            => value?.Trim().ToLower() ?? string.Empty;
    }
}
