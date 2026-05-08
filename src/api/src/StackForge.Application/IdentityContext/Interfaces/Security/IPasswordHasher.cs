using StackForge.Domain.IdentityContext.ValueObjects;

namespace StackForge.Application.IdentityContext.Interfaces.Security
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string password, PasswordHash passwordHash);
    }
}
