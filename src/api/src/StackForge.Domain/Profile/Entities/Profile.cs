using StackForge.Domain.Profile.Errors;
using StackForge.Domain.Profile.ValueObjects;
using StackForge.Domain.Shared.Entities;
using StackForge.Domain.Shared.Exceptions;

namespace StackForge.Domain.Profile.Entities
{
    public abstract class Profile : Entity
    {
        public Name Name { get; private set; }
        public Guid UserId { get; private set; }
        public DateOnly BirthDate { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        

        protected Profile(Name name, Guid userId, DateOnly birthDate)
        {
            ValidateBirthDate(birthDate);

            Name = name;
            UserId = userId;
            BirthDate = birthDate;
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public Profile() { }

        protected void UpdatePersonalInfo(Name name, DateOnly birthDate)
        {
            ValidateBirthDate(birthDate);
            Name = name;
            BirthDate = birthDate;
        }

        private static void ValidateBirthDate(DateOnly birthDate)
        {
            DomainExceptionValidation.When(birthDate > DateOnly.FromDateTime(DateTime.UtcNow), ProfileError.BirthDateInFuture);
        }
    }
}
