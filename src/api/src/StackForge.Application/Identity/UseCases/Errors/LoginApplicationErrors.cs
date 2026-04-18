using StackForge.Application.Shared.Results;

namespace StackForge.Application.Identity.UseCases.Errors
{
    public static class LoginApplicationErrors
    {
        public static readonly Error InvalidCredentials = new("Identity.Login.InvalidCredentials", "Invalid email or password.");
        public static readonly Error MultipleProfileFound = new("MultipleProfileFound", "Multiple profile found.");
        public static readonly Error ProfileNotFound = new("Identity.Login.ProfileNotFound", "Profile not found.");
    }
}
