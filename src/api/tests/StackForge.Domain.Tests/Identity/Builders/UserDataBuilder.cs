using Bogus;
using StackForge.Domain.Identity.Entities;

namespace StackForge.Domain.Tests.Identity.Builders
{
    public sealed class UserDataBuilder
    {
        private readonly Faker _faker = new();
        private string? _email;
        private string? _passwordHash;

        public UserDataBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }

        public UserDataBuilder WithPasswordHash(string passwordHash)
        {
            _passwordHash = passwordHash;
            return this;
        }

        public User Build()
        { 
            var email = _email ?? _faker.Internet.Email();
            var passwordHash = _passwordHash ?? _faker.Internet.Password();

            return User.Create(email, passwordHash);

        }
    }
}
