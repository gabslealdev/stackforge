namespace StackForge.Application.IdentityContext.UseCases.LoginUser
{
    public sealed record LoginUserResponse(string AccessToken, DateTimeOffset Expiration, string ProfileType);
}
