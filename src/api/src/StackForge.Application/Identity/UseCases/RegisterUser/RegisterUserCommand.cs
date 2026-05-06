using StackForge.Application.Abstractions.Messaging;
using StackForge.Application.Profile.UseCases.RegisterMentor;
using StackForge.Application.Shared.Results;
using StackForge.Domain.IdentityContext.Enums;

namespace StackForge.Application.Identity.UseCases.RegisterUser
{
    public sealed record RegisterUserCommand(string Email, string Password, ProfileType SelectedProfileType) 
        : ICommand<Result<RegisterUserResponse>>;
}
