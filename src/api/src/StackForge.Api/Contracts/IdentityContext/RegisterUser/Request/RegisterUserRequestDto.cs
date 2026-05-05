namespace StackForge.Api.Contracts.IdentityContext.RegisterUser.Request;

public sealed record RegisterUserRequestDto(string Email, string Password, string SelectedProfileType);