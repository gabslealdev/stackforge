using StackForge.Domain.IdentityContext.ValueObjects;

namespace StackForge.Application.IdentityContext.Interfaces.Security
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Guid userId, Email email, string profileType);
        DateTimeOffset GetTokenExpiration();
    }
}
