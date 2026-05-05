using StackForge.Domain.IdentityContext.ValueObjects;
using StackForge.Domain.Shared.Entities;

namespace StackForge.Domain.IdentityContext.Entities
{
    public sealed class User : Entity
    {
        public Email Email { get; private set; } = null!;
        public PasswordHash PasswordHash { get; private set; } = null!;

        private User(Email email, PasswordHash passwordHash)
        {
            Email = email;
            PasswordHash = passwordHash;
        }

        private User() { }

        public static User Create(Email email, PasswordHash passwordHash) 
            => new User(email, passwordHash);

    }
}
