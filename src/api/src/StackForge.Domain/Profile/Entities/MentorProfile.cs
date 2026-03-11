using StackForge.Domain.Profile.Enums;
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

        public static MentorProfile Create(Name name, Guid userId, DateOnly birthDate, Bio? bio, AvailabityStatus availabity, Education education)
        {
            Validate(birthDate, userId);
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

        public void UpdateEducation(Education education)
        {
            Education = education;
        }

        public void UpdateBio(Bio? bio)
        {
            Bio = bio;
        }

        public void ChangeAvailability()
        {
           if (Availability == AvailabityStatus.Unavailable)
                Availability = AvailabityStatus.Available;
            else
                Availability = AvailabityStatus.Unavailable;
        }

        public void UpdateMentorProfile(Name name, DateOnly birthDate)
        {
            UpdatePersonalInfo(name, birthDate);
        }

        public void UpdateProfile(Name name, DateOnly birthDate, Education education, Bio? bio, AvailabityStatus availability)
        {
            UpdatePersonalInfo(name, birthDate);
            UpdateEducation(education);
            UpdateBio(bio);
        }

    }
}
