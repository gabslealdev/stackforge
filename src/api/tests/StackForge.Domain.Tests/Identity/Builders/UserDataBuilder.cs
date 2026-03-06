using Bogus;
using StackForge.Domain.Identity.Entities;
using StackForge.Domain.Identity.Enums;
using StackForge.Domain.Identity.ValueObjects;

namespace StackForge.Domain.Tests.Identity.Builders
{
    public sealed class UserDataBuilder
    {
        private readonly Faker _faker = new();
        private string? _email;
        private string? _passwordHash;
        private Role? _role;

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

        public UserDataBuilder WithRole(Role role)
        {
            _role = role;
            return this;
        }

        public User Build()
        { 
            var email = _email ?? _faker.Internet.Email();
            var passwordHash = _passwordHash ?? _faker.Internet.Password();
            var role = _role ?? _faker.PickRandom<Role>();

            return User.Create(email, passwordHash, role);

        }
    }
}
