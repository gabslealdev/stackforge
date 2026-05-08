using StackForge.Application.Abstractions.Messaging;
using StackForge.Application.Shared.Results;
using StackForge.Domain.IdentityContext.Enums;

namespace StackForge.Application.IdentityContext.UseCases.RegisterUser
{
    public sealed record RegisterUserCommand(string Email, string Password, ProfileType SelectedProfileType) 
        : ICommand<Result<RegisterUserResponse>>;
}
