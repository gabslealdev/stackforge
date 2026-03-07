using StackForge.Domain.Profile.ValueObjects;

namespace StackForge.Domain.Profile.Entities
{
    public sealed class LearnerProfile : Profile
    {
        private LearnerProfile(Name name, Guid userId, DateOnly birthDate) : base(name, userId, birthDate)
        {
        }
        private LearnerProfile() { }
        public static LearnerProfile Create(string firstName, string lastName, Guid userId, DateOnly birthDate)
        {
            Validate(birthDate, userId);

            var name = Name.Create(firstName, lastName);
            return new LearnerProfile(name, userId, birthDate);
        }

        public void UpdateLearnerProfile(string firstName, string lastName, DateOnly birthDate)
        {
            UpdatePersonalInfo(firstName, lastName, birthDate);
        }
    }
}
