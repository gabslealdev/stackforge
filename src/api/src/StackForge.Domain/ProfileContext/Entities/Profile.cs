using StackForge.Domain.Profile.Errors;
using StackForge.Domain.Profile.ValueObjects;
using StackForge.Domain.Shared.Entities;
using StackForge.Domain.Shared.Exceptions;

namespace StackForge.Domain.Profile.Entities
{
    public abstract class Profile : Entity
    {
        public Name Name { get; private set; } = null!;
        public Guid UserId { get; private set; }
        public DateOnly BirthDate { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        

        protected Profile(Name name, Guid userId, DateOnly birthDate)
        {
            Validate(birthDate, userId);

            Name = name;
            UserId = userId;
            BirthDate = birthDate;
            CreatedAt = DateTimeOffset.UtcNow;
        }

        protected Profile() { }

        protected void UpdatePersonalInfo(Name name, DateOnly birthDate)
        {
            DomainExceptionValidation.When(!isValidBirthDate(birthDate), ProfileError.BirthDateInFuture);

            Name = name;
            BirthDate = birthDate;
        }

        protected static void Validate(DateOnly birthDate, Guid userId)
        {
            DomainExceptionValidation.When(!isValidBirthDate(birthDate), ProfileError.BirthDateInFuture);
            DomainExceptionValidation.When(userId == Guid.Empty, ProfileError.UserIdRequired);
        }

        private static bool isValidBirthDate(DateOnly birthDate)
        {
           var today = DateOnly.FromDateTime(DateTime.UtcNow);
            return birthDate <= today;
        }
    }
}
