using StackForge.Domain.Identity.Enums;

namespace StackForge.Application.Identity.UseCases.RegisterUser
{
    public sealed record RegisterUserCommand(string Email, string Password, ProfileType SelectedProfileType);
}
