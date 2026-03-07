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
        public string? Bio { get; private set; }
        public AvailabityStatus Availability { get; private set; }

        private MentorProfile(Name name, Guid userId, DateOnly birthDate, Education education, string? bio, AvailabityStatus availabity) : base(name, userId, birthDate)
        {
            Education = education;
            Bio = bio;
            Availability = availabity;
        }
        private MentorProfile() { }

        public static MentorProfile Create(string firstName, string lastName, Guid userId, DateOnly biirthDate, string? bio, AvailabityStatus availabity, string courseName, string institution, EducationStatus status, DateOnly conclusionDate )
        {
            var name = Name.Create(firstName, lastName);
            var education = Education.Create(courseName, institution, status, conclusionDate);
            
            return new MentorProfile(name, userId, biirthDate, education, bio, availabity);
        }
        public void AddStack(Stack stack)
        {
            DomainExceptionValidation.When(_stacks.Any(s => s.Key == stack.Key), MentorError.StackAlreadyAdded);
            DomainExceptionValidation.When(stack is null, MentorError.StackRequired);

            _stacks.Add(stack!);
        }
        public void RemoveStack(Guid stackId)
        {
            var stack = _stacks.FirstOrDefault(s => s.Id == stackId);

            DomainExceptionValidation.When(stack is null, MentorError.StackRequired);
            DomainExceptionValidation.When(_stacks.Count == 1, MentorError.StackRequired);

            _stacks.Remove(stack!);
        }
        public void UpdateEducation(Education education)
        {
            Education = education;
        }


    }
}
