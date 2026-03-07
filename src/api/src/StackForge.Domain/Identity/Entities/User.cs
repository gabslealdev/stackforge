
using StackForge.Domain.Identity.ValueObjects;
using StackForge.Domain.Shared.Entities;

namespace StackForge.Domain.Identity.Entities
{
    public sealed class User : Entity
    {
        public Email Email { get; private set; }
        public PasswordHash PasswordHash { get; private set; }

        private User(Email email, PasswordHash passwordHash)
        {
            Email = email;
            PasswordHash = passwordHash;
        }

        private User() { }

        public static User Create(string email, string passwordHash) 
            => new User(Email.Create(email), PasswordHash.Create(passwordHash));


    }
}
