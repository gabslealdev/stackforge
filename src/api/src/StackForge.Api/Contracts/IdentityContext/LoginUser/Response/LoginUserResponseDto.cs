namespace StackForge.Api.Contracts.IdentityContext.LoginUser.Response;

public sealed record LoginUserResponseDto(string AccessToken, DateTimeOffset Expiration, string ProfileType);