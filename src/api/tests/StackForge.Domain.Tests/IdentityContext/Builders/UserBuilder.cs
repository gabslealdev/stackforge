using Bogus;
using StackForge.Domain.IdentityContext.Entities;
using StackForge.Domain.IdentityContext.ValueObjects;

namespace StackForge.Domain.Tests.IdentityContext.Builders
{
    public sealed class UserBuilder
    {
        private readonly Faker _faker = new();
        private string? _email;
        private string? _passwordHash;

        public UserBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }

        public UserBuilder WithPasswordHash(string passwordHash)
        {
            _passwordHash = passwordHash;
            return this;
        }

        public User Build()
        { 
            var email = _email ?? _faker.Internet.Email();
            var passwordHash = _passwordHash ?? _faker.Internet.Password();

            

            return User.Create(Email.Create(email), PasswordHash.Create(passwordHash));

        }
    }
}
