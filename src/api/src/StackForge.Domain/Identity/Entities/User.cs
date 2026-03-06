
using StackForge.Domain.Identity.ValueObjects;
using StackForge.Domain.Shared.Entities;

namespace StackForge.Domain.Identity.Entities
{
    public sealed class User : Entity
    {
        public Email email { get; private set; }
        public PasswordHash passwordHash { get; private set; }


    }
}
