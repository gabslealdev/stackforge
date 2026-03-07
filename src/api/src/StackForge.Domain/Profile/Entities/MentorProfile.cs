using StackForge.Domain.Profile.Enum;
using StackForge.Domain.Profile.Errors;
using StackForge.Domain.Profile.ValueObjects;
using StackForge.Domain.Shared.Exceptions;
using StackForge.Domain.Stacks.Entities;

namespace StackForge.Domain.Profile.Entities
{
    public sealed class MentorProfile : Profile
    {
        private readonly List<Stack> _stacks = [];
        public IReadOnlyCollection<Stack> Stacks => _stacks.AsReadOnly();


        public Education Education { get; private set; }
        public Bio? Bio { get; private set; }
        public AvailabityStatus Availability { get; private set; }

        private MentorProfile(Name name, Guid userId, DateOnly birthDate, Education education, Bio? bio, AvailabityStatus availabity) : base(name, userId, birthDate)
        {
            Education = education;
            Bio = bio;
            Availability = availabity;
        }
        private MentorProfile() { }

        public static MentorProfile Create(string firstName, string lastName, Guid userId, DateOnly birthDate, string? textBio, AvailabityStatus availabity, string courseName, string institution, EducationStatus status, DateOnly conclusionDate)
        {
            Validate(birthDate, userId);

            var name = Name.Create(firstName, lastName);
            var education = Education.Create(courseName, institution, status, conclusionDate);
            Bio? bio = string.IsNullOrWhiteSpace(textBio) ? null : Bio.Create(textBio);


            return new MentorProfile(name, userId, birthDate, education, bio, availabity);
        }

        public void AddStack(Stack stack)
        {
            DomainExceptionValidation.When(_stacks.Any(s => s.Key == stack.Key), MentorError.StackAlreadyAdded);

            _stacks.Add(stack);
        }

        public void RemoveStack(Guid stackId)
        {
            var stack = _stacks.FirstOrDefault(s => s.Id == stackId);

            DomainExceptionValidation.When(stack is null, MentorError.StackNotFound);
            DomainExceptionValidation.When(_stacks.Count == 1, MentorError.StackOnlyOne);

            _stacks.Remove(stack!);
        }

        public void UpdateEducation(string courseName, string institution, EducationStatus status, DateOnly conclusionDate)
        {
            Education = Education.Create(courseName, institution, status, conclusionDate);
        }

        public void UpdateBio(string? inputBio)
        {
            Bio = string.IsNullOrWhiteSpace(inputBio) ? null : Bio.Create(inputBio);
        }

        public void ChangeAvailability()
        {
           if (Availability == AvailabityStatus.Unavailable)
                Availability = AvailabityStatus.Available;
            else
                Availability = AvailabityStatus.Unavailable;
        }

        public void UpdateMentorProfile(string firstName, string lastName, DateOnly birthDate)
        {
            UpdatePersonalInfo(firstName, lastName, birthDate);
        }

        public void UpdateProfile(string firstName, string lastName, DateOnly birthDate, string courseName, string institution, EducationStatus status, DateOnly conclusionDate, string? textBio, AvailabityStatus availability)
        {
            UpdatePersonalInfo(firstName, lastName, birthDate);
            UpdateEducation(courseName, institution, status, conclusionDate);
            UpdateBio(textBio);
        }

    }
}
