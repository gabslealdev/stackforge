using Shouldly;
using StackForge.Application.IdentityContext.Errors;
using StackForge.Application.IdentityContext.UseCases.RegisterUser;
using StackForge.Application.Tests.Common;
using StackForge.Domain.IdentityContext.Enums;

namespace StackForge.Application.Tests.IdentityContext.UseCases;

public sealed class RegisterUserHandlerTests
{
    [Fact]
    public async Task HandleAsync_ShouldRegisterUser_WhenEmailIsAvailable()
    {
        var userRepository = new FakeUserRepository();
        var registrationRepository = new FakeUserRegistrationRepository();
        var passwordHasher = new FakePasswordHasher { HashResult = "hashed-secret" };
        var unitOfWork = new FakeUnitOfWork();
        var handler = new RegisterUserHandler(userRepository, registrationRepository, passwordHasher, unitOfWork);
        var command = new RegisterUserCommand("USER@EXAMPLE.COM", "secret", ProfileType.Mentor);

        var result = await handler.HandleAsync(command);

        result.IsSuccess.ShouldBeTrue();
        result.Value.UserId.ShouldNotBe(Guid.Empty);
        userRepository.AddedUsers.Count.ShouldBe(1);
        userRepository.AddedUsers[0].Email.Value.ShouldBe("user@example.com");
        userRepository.AddedUsers[0].PasswordHash.Value.ShouldBe("hashed-secret");
        registrationRepository.AddedRegistrations.Count.ShouldBe(1);
        registrationRepository.AddedRegistrations[0].UserId.ShouldBe(result.Value.UserId);
        registrationRepository.AddedRegistrations[0].SelectedProfileType.ShouldBe(ProfileType.Mentor);
        passwordHasher.HashedPasswords.ShouldBe(["secret"]);
        unitOfWork.SaveChangesCount.ShouldBe(1);
    }

    [Fact]
    public async Task HandleAsync_ShouldFail_WhenEmailAlreadyExists()
    {
        var userRepository = new FakeUserRepository { ExistsByEmailResult = true };
        var registrationRepository = new FakeUserRegistrationRepository();
        var passwordHasher = new FakePasswordHasher();
        var unitOfWork = new FakeUnitOfWork();
        var handler = new RegisterUserHandler(userRepository, registrationRepository, passwordHasher, unitOfWork);

        var result = await handler.HandleAsync(new RegisterUserCommand("user@example.com", "secret", ProfileType.Learner));

        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(UserApplicationErrors.EmailAlreadyInUse);
        userRepository.AddedUsers.ShouldBeEmpty();
        registrationRepository.AddedRegistrations.ShouldBeEmpty();
        passwordHasher.HashedPasswords.ShouldBeEmpty();
        unitOfWork.SaveChangesCount.ShouldBe(0);
    }
}
