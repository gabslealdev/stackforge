using StackForge.Application.Shared.Results;

namespace StackForge.Application.Identity.UseCases.RegisterUser
{
    public static class IdentityApplicationErrors
    {
        public static readonly Error EmailAlreadyInUse = new("Identity.EmailAlreadyInUse", "This email is already in use.");
    }
}
