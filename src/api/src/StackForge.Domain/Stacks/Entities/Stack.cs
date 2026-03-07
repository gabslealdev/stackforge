using StackForge.Domain.Profile.Entities;
using StackForge.Domain.Shared.Entities;
using StackForge.Domain.Shared.Exceptions;
using StackForge.Domain.Stacks.Errors;
using StackForge.Domain.Stacks.ValueObjects;

namespace StackForge.Domain.Stacks.Entities
{
    public sealed class Stack : Entity
    {
        private const int MaxLength = 20;
        public string Name { get; private set; }
        public Key Key { get; private set; }

        private readonly List<MentorProfile> _mentors = [];
        public IReadOnlyCollection<MentorProfile> Mentors => _mentors.AsReadOnly();

        private Stack(string name, Key key)
        {
            Name = name;
            Key = key;
        }

        private Stack() { }

        public static Stack Create(string name, string key)
        {
            Validate(name);
            var normalizedName = Normalize(name);

            return new Stack(normalizedName, Key.Create(key));
        }

        private static string Normalize(string value)
            => value?.Trim() ?? string.Empty;

        private static void Validate(string name)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(name), StackErrors.StackNameRequired);
            DomainExceptionValidation.When(name.Length > MaxLength, StackErrors.StackNameTooLong);
        }
    }
}
