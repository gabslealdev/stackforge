using StackForge.Domain.Identity.ValueObjects;

namespace StackForge.Application.Identity.Interfaces.Security
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string password, PasswordHash passwordHash);
    }
}
