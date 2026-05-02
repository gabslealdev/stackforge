using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StackForge.Application.Identity.Interfaces.Security;
using StackForge.Domain.Identity.ValueObjects;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace StackForge.Infrastructure.Authentication
{
    public sealed class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtOptions _options;

        public JwtTokenGenerator(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }
        public string GenerateToken(Guid userId, Email email, string profileType)
        {
            var claims = new List<Claim>
            {
                new (JwtRegisteredClaimNames.Sub, userId.ToString()), 
                new (JwtRegisteredClaimNames.Email, email.Value),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new ("profileType", profileType)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow.AddMinutes(_options.ExpirationInMinutes);

            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public DateTimeOffset GetTokenExpiration() => DateTimeOffset.UtcNow.AddMinutes(_options.ExpirationInMinutes);
    }
}
