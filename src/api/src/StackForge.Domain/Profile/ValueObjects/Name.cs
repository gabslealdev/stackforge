using StackForge.Domain.Profile.Errors;
using StackForge.Domain.Shared.Exceptions;
using StackForge.Domain.Shared.ValueObjects;

namespace StackForge.Domain.Profile.ValueObjects
{
    public sealed record Name : ValueObject
    {
        private const int MinLength = 3;
        private const int MaxLength = 80;
        public string FirstName { get; } 
        public string LastName { get; }

        private Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        private Name(){}

        public static Name Create(string firstName, string lastName)
        {
            var normalizedFirstName = Normalize(firstName);
            var normalizedLastName = Normalize(lastName);

            Validate(normalizedFirstName, normalizedLastName);

            return new Name(normalizedFirstName, normalizedLastName);
        }

        public override string ToString() => $"{FirstName} {LastName}";

        private static string Normalize(string value) 
            => value.Trim();

        private static void Validate(string firstName, string lastName)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(firstName), ProfileError.FirstNameRequired);
            DomainExceptionValidation.When(firstName.Length < MinLength, ProfileError.FirstNameTooShort);
            DomainExceptionValidation.When(firstName.Length > MaxLength, ProfileError.FirstNameTooLong);

            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(lastName), ProfileError.LastNameRequired);
            DomainExceptionValidation.When(lastName.Length < MinLength, ProfileError.LastNameTooShort);
            DomainExceptionValidation.When(lastName.Length > MaxLength, ProfileError.LastNameTooLong);
        }





    }
}
