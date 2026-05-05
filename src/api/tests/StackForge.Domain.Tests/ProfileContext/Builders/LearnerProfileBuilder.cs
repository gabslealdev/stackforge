using Bogus;
using StackForge.Domain.ProfileContext.Entities;
using StackForge.Domain.ProfileContext.ValueObjects;

namespace StackForge.Domain.Tests.Profile.Builders
{
    public sealed class LearnerProfileBuilder
    {
        private readonly Faker _faker = new();
        private Name? _name;
        private string? _firstName;
        private string? _lastName;
        private Guid? _userId;
        private DateOnly? _birthDate;

        public LearnerProfileBuilder WithName(Name name)
        {
            _name = name;
            return this;
        }

        public LearnerProfileBuilder WithUserId(Guid userId)
        {
            _userId = userId;
            return this;
        }

        public LearnerProfileBuilder WithBirthDate(DateOnly birthDate)
        {
            _birthDate = birthDate;
            return this;
        }

        public LearnerProfile Build()
        {
            var firstName = _firstName ?? _faker.Name.FirstName();
            var lastName = _lastName ?? _faker.Name.LastName();
            var userId = _userId ?? Guid.NewGuid();
            var birthDate = _birthDate ?? DateOnly.FromDateTime(_faker.Date.Past(30, DateTime.UtcNow.AddYears(-18)));

            return LearnerProfile.Create(Name.Create(firstName, lastName), userId, birthDate);

        }

    }
}
