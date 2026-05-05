using StackForge.Domain.Identity.Enums;
using StackForge.Domain.Shared.Entities;

namespace StackForge.Domain.Identity.Entities
{
    public sealed class UserRegistration : Entity
    {
        public Guid UserId { get; private set; }
        public ProfileType SelectedProfileType { get; private set; }
        public RegistrationStatus Status { get; private set; }

        private UserRegistration(Guid userId, ProfileType selectedProfileType)
        {
            UserId = userId;
            SelectedProfileType = selectedProfileType;
            Status = RegistrationStatus.InProgress;
        }

        private UserRegistration(){ }

        public static UserRegistration Create(Guid userId, ProfileType selectedProfileType) 
            => new UserRegistration(userId, selectedProfileType);


        public void Complete()
        {
            Status = RegistrationStatus.Completed;
        }

    }
}
