namespace StackForge.Application.Identity.UseCases.LoginUser
{
    public sealed record LoginUserResponse(string AccessToken, DateTimeOffset Expiration, string ProfileType);
}
