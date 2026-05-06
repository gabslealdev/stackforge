using StackForge.Application.Abstractions.Messaging;
using StackForge.Application.Profile.UseCases.RegisterMentor;
using StackForge.Application.Shared.Results;

namespace StackForge.Application.Identity.UseCases.LoginUser
{
    public sealed record LoginUserCommand(string Email, string Password)
        : ICommand<Result<LoginUserResponse>>;
}
