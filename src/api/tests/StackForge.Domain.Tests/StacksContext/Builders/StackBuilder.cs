using Bogus;
using StackForge.Domain.StacksContext.Entities;
using StackForge.Domain.StacksContext.ValueObjects;

namespace StackForge.Domain.Tests.Stacks.Builders
{
    public sealed class StackBuilder
    {
        private readonly Faker _faker = new();
        private string? _name;
        private string? _key;

        public StackBuilder WithName(string? name)
        {
            _name = name;
            return this;
        }

        public StackBuilder WithKey(string? key)
        {
            _key = key;
            return this;
        }

        public Stack Build()
        {
            var name = _name ?? _faker.Lorem.Word();
            var key = _key ?? _faker.Lorem.Word();
            return Stack.Create(name, Key.Create(key));
        }
    }
}
