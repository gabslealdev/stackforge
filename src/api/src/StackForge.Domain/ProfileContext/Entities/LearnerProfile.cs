using StackForge.Domain.ProfileContext.ValueObjects;

namespace StackForge.Domain.ProfileContext.Entities
{
    public sealed class LearnerProfile : Profile
    {
        private LearnerProfile(Name name, Guid userId, DateOnly birthDate) : base(name, userId, birthDate)
        {
        }
        private LearnerProfile() { }
        public static LearnerProfile Create(Name name, Guid userId, DateOnly birthDate)
        {
            Validate(birthDate, userId);
            return new LearnerProfile(name, userId, birthDate);
        }

        public void UpdateLearnerProfile(Name name, DateOnly birthDate)
        {
            UpdatePersonalInfo(name, birthDate);
        }
    }
}
