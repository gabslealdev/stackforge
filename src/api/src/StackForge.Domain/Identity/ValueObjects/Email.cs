using StackForge.Domain.Identity.Errors;
using StackForge.Domain.Shared.Exceptions;
using StackForge.Domain.Shared.ValueObjects;
using System.Text.RegularExpressions;

namespace StackForge.Domain.Identity.ValueObjects
{
    public sealed record Email : ValueObject
    {
        private const int MinLength = 5;
        private const int MaxLength = 254;

        public string Value { get;}

        private Email(string value)
        {
            Value = value;
        }

        private Email(){}

        public static Email Create(string value)
        {
            var normalizedValue = Normalize(value);
            Validate(normalizedValue);
            return new Email(normalizedValue);
        }

        private static string Normalize(string value) 
            => value.Trim().ToLowerInvariant();

        private static bool IsValid(string value) 
            => Regex.IsMatch(value, @"^[^\s@]+@[^\s@]+\.[^\s@]+$");
       
        private static void Validate(string value)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(value), UserError.EmailRequired);
            DomainExceptionValidation.When(value.Length < MinLength, UserError.EmailInvalid);
            DomainExceptionValidation.When(value.Length > MaxLength, UserError.EmailInvalid);
            DomainExceptionValidation.When(!IsValid(value), UserError.EmailInvalid);
        }

    }
}
