using System.Reflection.Metadata;

namespace StackForge.Application.Shared.Results
{
    public sealed record Error(string Code, string Message)
    {
        public static readonly Error None = new(string.Empty, string.Empty);
    }
}
