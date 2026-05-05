using StackForge.Domain.ProfileContext.Errors;
using StackForge.Domain.Shared.Exceptions;
using StackForge.Domain.Shared.ValueObjects;

namespace StackForge.Domain.ProfileContext.ValueObjects
{
    public sealed record Bio : ValueObject
    {
        private const int MinLength = 10;
        private const int MaxLength = 500;
        public string Value { get; private set; } = string.Empty;
        private Bio(string value)
        {
            Value = value;
        }
        public Bio() { }

        public static Bio Create(string value)
        {
            var normalizedValue = Normalize(value);
            Validate(normalizedValue);

            return new Bio(normalizedValue);
        }

        private static string Normalize(string value) 
            => value?.Trim() ?? string.Empty;

        private static void Validate(string value)
        {
            DomainExceptionValidation.When(HasBio(value) && value.Length < MinLength, MentorError.BioTooShort);
            DomainExceptionValidation.When(value.Length > MaxLength, MentorError.BioTooLong);
        }

        private static bool HasBio(string value) 
            => value.Length > 0;

    }
}
