using StackForge.Domain.ProfileContext.Entities;
using StackForge.Domain.Shared.Entities;
using StackForge.Domain.Shared.Exceptions;
using StackForge.Domain.StacksContext.Errors;
using StackForge.Domain.StacksContext.ValueObjects;

namespace StackForge.Domain.StacksContext.Entities
{
    public sealed class Stack : Entity
    {
        private const int MaxLength = 20;
        public string Name { get; private set; } = string.Empty;
        public Key Key { get; private set; } = null!;

        private readonly List<MentorProfile> _mentors = [];
        public IReadOnlyCollection<MentorProfile> Mentors => _mentors.AsReadOnly();

        private Stack(string name, Key key)
        {
            Name = name;
            Key = key;
        }

        private Stack() { }

        public static Stack Create(string name, Key key)
        {
            Validate(name);
            var normalizedName = Normalize(name);

            return new Stack(normalizedName, key);
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
