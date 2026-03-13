using Bogus;
using StackForge.Domain.Profile.Entities;
using StackForge.Domain.Profile.Enums;
using StackForge.Domain.Profile.ValueObjects;

namespace StackForge.Domain.Tests.Profile.Builders
{
    public sealed class MentorProfileBuilder
    {
        private readonly Faker _faker = new();
        private Name? _name;
        private string? _firstName;
        private string? _lastName;
        private Guid? _userId;
        private DateOnly? _birthDate;
        private string? _textBio;
        private AvailabilityStatus? _availability;
        private Education? _education;
        private string? _courseName;
        private string? _institution;
        private EducationStatus? _educationStatus;
        private DateOnly? _conclusionDate;

        public MentorProfileBuilder WithName(Name? name)
        {
            _name = name;
            return this;
        }

        public MentorProfileBuilder WithUserId(Guid userId)
        {
            _userId = userId;
            return this;
        }

        public MentorProfileBuilder WithBirthDate(DateOnly birthDate)
        {
            _birthDate = birthDate;
            return this;
        } 

        public MentorProfileBuilder WithBio(string textBio)
        {
            _textBio = textBio;
            return this;
        }
        public MentorProfileBuilder WithEducation(Education education)
        {
            _education = education;
            return this;
        }

        public MentorProfile Build()
        {
            var firstName = _firstName ?? _faker.Name.FirstName();
            var lastName = _lastName ?? _faker.Name.LastName();
            var userId = _userId ?? Guid.NewGuid();
            var birthDate = _birthDate ?? DateOnly.FromDateTime(_faker.Date.Past(30, DateTime.UtcNow.AddYears(-18)));
            var textBio = _textBio ?? _faker.Lorem.Paragraph();
            var availability = _availability ?? AvailabilityStatus.Available;
            var courseName = _courseName ?? _faker.Name.JobTitle();
            var institution = _institution ?? _faker.Random.Word();
            var educationStatus = _educationStatus ?? EducationStatus.Completed;
            var conclusionDate = _conclusionDate ?? DateOnly.FromDateTime(_faker.Date.Past(5));

            return MentorProfile.Create(Name.Create(firstName, lastName), userId, birthDate, Bio.Create(textBio), availability, Education.Create(courseName, institution, educationStatus, conclusionDate));
        }

    }
}
