using StackForge.Application.Abstractions.Messaging;
using StackForge.Application.Shared.Results;

namespace StackForge.Application.IdentityContext.UseCases.LoginUser
{
    public sealed record LoginUserCommand(string Email, string Password)
        : ICommand<Result<LoginUserResponse>>;
}
