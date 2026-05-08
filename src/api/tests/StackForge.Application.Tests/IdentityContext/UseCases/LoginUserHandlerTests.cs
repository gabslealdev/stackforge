using Shouldly;
using StackForge.Application.IdentityContext.Errors;
using StackForge.Application.IdentityContext.UseCases.LoginUser;
using StackForge.Application.Tests.Common;
using StackForge.Domain.IdentityContext.ValueObjects;

namespace StackForge.Application.Tests.IdentityContext.UseCases;

public sealed class LoginUserHandlerTests
{
    [Fact]
    public async Task HandleAsync_ShouldLoginMentor_WhenCredentialsAndProfileAreValid()
    {
        var user = TestData.CreateUser("mentor@example.com", "stored-hash");
        var userRepository = new FakeUserRepository { UserByEmail = user };
        var mentorRepository = new FakeMentorProfileRepository { ExistsByUserIdResult = true };
        var learnerRepository = new FakeLearnerProfileRepository();
        var passwordHasher = new FakePasswordHasher { VerifyResult = true };
        var jwtTokenGenerator = new FakeJwtTokenGenerator();
        var handler = new LoginUserHandler(userRepository, mentorRepository, learnerRepository, passwordHasher, jwtTokenGenerator);

        var result = await handler.HandleAsync(new LoginUserCommand("mentor@example.com", "secret"));

        result.IsSuccess.ShouldBeTrue();
        result.Value.AccessToken.ShouldBe("access-token");
        result.Value.Expiration.ShouldBe(jwtTokenGenerator.Expiration);
        result.Value.ProfileType.ShouldBe("Mentor");
        passwordHasher.VerifiedPasswords.ShouldBe([("secret", PasswordHash.Create("stored-hash"))]);
        jwtTokenGenerator.GeneratedTokens.Count.ShouldBe(1);
        jwtTokenGenerator.GeneratedTokens[0].UserId.ShouldBe(user.Id);
        jwtTokenGenerator.GeneratedTokens[0].ProfileType.ShouldBe("Mentor");
    }

    [Fact]
    public async Task HandleAsync_ShouldFail_WhenUserDoesNotExist()
    {
        var handler = new LoginUserHandler(
            new FakeUserRepository(),
            new FakeMentorProfileRepository(),
            new FakeLearnerProfileRepository(),
            new FakePasswordHasher(),
            new FakeJwtTokenGenerator());

        var result = await handler.HandleAsync(new LoginUserCommand("missing@example.com", "secret"));

        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(LoginApplicationErrors.InvalidCredentials);
    }

    [Fact]
    public async Task HandleAsync_ShouldFail_WhenPasswordIsInvalid()
    {
        var handler = new LoginUserHandler(
            new FakeUserRepository { UserByEmail = TestData.CreateUser() },
            new FakeMentorProfileRepository { ExistsByUserIdResult = true },
            new FakeLearnerProfileRepository(),
            new FakePasswordHasher { VerifyResult = false },
            new FakeJwtTokenGenerator());

        var result = await handler.HandleAsync(new LoginUserCommand("user@example.com", "wrong-secret"));

        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(LoginApplicationErrors.InvalidCredentials);
    }

    [Fact]
    public async Task HandleAsync_ShouldFail_WhenUserHasNoProfile()
    {
        var handler = new LoginUserHandler(
            new FakeUserRepository { UserByEmail = TestData.CreateUser() },
            new FakeMentorProfileRepository(),
            new FakeLearnerProfileRepository(),
            new FakePasswordHasher { VerifyResult = true },
            new FakeJwtTokenGenerator());

        var result = await handler.HandleAsync(new LoginUserCommand("user@example.com", "secret"));

        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(LoginApplicationErrors.ProfileNotFound);
    }

    [Fact]
    public async Task HandleAsync_ShouldFail_WhenUserHasMultipleProfiles()
    {
        var handler = new LoginUserHandler(
            new FakeUserRepository { UserByEmail = TestData.CreateUser() },
            new FakeMentorProfileRepository { ExistsByUserIdResult = true },
            new FakeLearnerProfileRepository { ExistsByUserIdResult = true },
            new FakePasswordHasher { VerifyResult = true },
            new FakeJwtTokenGenerator());

        var result = await handler.HandleAsync(new LoginUserCommand("user@example.com", "secret"));

        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(LoginApplicationErrors.MultipleProfileFound);
    }
}
