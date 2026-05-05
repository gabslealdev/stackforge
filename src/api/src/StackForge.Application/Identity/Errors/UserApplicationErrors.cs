using StackForge.Application.Shared.Results;

namespace StackForge.Application.Identity.Errors
{
    public static class UserApplicationErrors
    {
        public static readonly Error EmailAlreadyInUse = new("Identity.EmailAlreadyInUse", "This email is already in use.");
    }
}
