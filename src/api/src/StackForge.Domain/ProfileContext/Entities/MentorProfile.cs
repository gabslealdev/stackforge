using StackForge.Domain.ProfileContext.Enums;
using StackForge.Domain.ProfileContext.Errors;
using StackForge.Domain.ProfileContext.ValueObjects;
using StackForge.Domain.Shared.Exceptions;
using StackForge.Domain.StacksContext.Entities;

namespace StackForge.Domain.ProfileContext.Entities
{
    public sealed class MentorProfile : Profile
    {
        private readonly List<Stack> _stacks = [];
        public IReadOnlyCollection<Stack> Stacks => _stacks.AsReadOnly();


        public Education Education { get; private set; } = null!;
        public Bio? Bio { get; private set; }
        public AvailabilityStatus Availability { get; private set; }


        private MentorProfile(Name name, Guid userId, DateOnly birthDate, Education education, Bio? bio) : base(name, userId, birthDate)
        {
            Education = education;
            Bio = bio;
            Availability = AvailabilityStatus.Unavailable;
        }
        private MentorProfile() { }

        public static MentorProfile Create(Name name, Guid userId, DateOnly birthDate, Education education,  Bio? bio)
        {
            Validate(birthDate, userId);
            return new MentorProfile(name, userId, birthDate, education, bio);
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

        public void UpdateEducation(Education education)
        {
            Education = education;
        }

        public void UpdateBio(Bio? bio)
        {
            Bio = bio;
        }

        public void UpdateMentorProfile(Name name, DateOnly birthDate)
        {
            UpdatePersonalInfo(name, birthDate);
        }

        public void UpdateProfile(Name name, DateOnly birthDate, Education education, Bio? bio, AvailabilityStatus availability)
        {
            UpdatePersonalInfo(name, birthDate);
            UpdateEducation(education);
            UpdateBio(bio);
        }

        public void MarkAsAvailable()
        {
            var existsStack = _stacks.Count > 0;
            DomainExceptionValidation.When(!existsStack, MentorError.StackRequired);

            Availability = AvailabilityStatus.Available;
        }

        public void MarkAsUnavailable()
        {
            Availability = AvailabilityStatus.Unavailable;
        }
    }
}
