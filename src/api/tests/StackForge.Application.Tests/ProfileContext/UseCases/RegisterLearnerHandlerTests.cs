using Shouldly;
using StackForge.Application.ProfileContext.Errors;
using StackForge.Application.ProfileContext.UseCases.RegisterLearner;
using StackForge.Application.Tests.Common;
using StackForge.Domain.IdentityContext.Enums;

namespace StackForge.Application.Tests.ProfileContext.UseCases;

public sealed class RegisterLearnerHandlerTests
{
    [Fact]
    public async Task HandleAsync_ShouldRegisterLearner_WhenRegistrationIsValid()
    {
        var user = TestData.CreateUser();
        var registration = TestData.CreateRegistration(user.Id, ProfileType.Learner);
        var learnerRepository = new FakeLearnerProfileRepository();
        var registrationRepository = new FakeUserRegistrationRepository { RegistrationByUserId = registration };
        var unitOfWork = new FakeUnitOfWork();
        var handler = new RegisterLearnerHandler(
            new FakeUserRepository { UserById = user },
            registrationRepository,
            learnerRepository,
            unitOfWork);

        var result = await handler.HandleAsync(new RegisterLearnerCommand(user.Id, "Joao", "Souza", new DateOnly(2000, 1, 1)));

        result.IsSuccess.ShouldBeTrue();
        result.Value.LearnerId.ShouldNotBe(Guid.Empty);
        learnerRepository.AddedLearners.Count.ShouldBe(1);
        learnerRepository.AddedLearners[0].Id.ShouldBe(result.Value.LearnerId);
        registration.Status.ShouldBe(RegistrationStatus.Completed);
        registrationRepository.UpdatedRegistrations.ShouldBe([registration]);
        unitOfWork.SaveChangesCount.ShouldBe(1);
    }

    [Fact]
    public async Task HandleAsync_ShouldFail_WhenUserDoesNotExist()
    {
        var handler = new RegisterLearnerHandler(
            new FakeUserRepository(),
            new FakeUserRegistrationRepository(),
            new FakeLearnerProfileRepository(),
            new FakeUnitOfWork());

        var result = await handler.HandleAsync(ValidCommand(Guid.NewGuid()));

        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(ProfileApplicationErrors.UserNotFound);
    }

    [Fact]
    public async Task HandleAsync_ShouldFail_WhenRegistrationDoesNotExist()
    {
        var user = TestData.CreateUser();
        var handler = new RegisterLearnerHandler(
            new FakeUserRepository { UserById = user },
            new FakeUserRegistrationRepository(),
            new FakeLearnerProfileRepository(),
            new FakeUnitOfWork());

        var result = await handler.HandleAsync(ValidCommand(user.Id));

        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(ProfileApplicationErrors.UserRegistrationNotFound);
    }

    [Fact]
    public async Task HandleAsync_ShouldFail_WhenSelectedProfileIsNotLearner()
    {
        var user = TestData.CreateUser();
        var registration = TestData.CreateRegistration(user.Id, ProfileType.Mentor);
        var handler = new RegisterLearnerHandler(
            new FakeUserRepository { UserById = user },
            new FakeUserRegistrationRepository { RegistrationByUserId = registration },
            new FakeLearnerProfileRepository(),
            new FakeUnitOfWork());

        var result = await handler.HandleAsync(ValidCommand(user.Id));

        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(ProfileApplicationErrors.ProfileInvalid);
    }

    [Fact]
    public async Task HandleAsync_ShouldFail_WhenRegistrationIsCompleted()
    {
        var user = TestData.CreateUser();
        var registration = TestData.CreateRegistration(user.Id, ProfileType.Learner, completed: true);
        var handler = new RegisterLearnerHandler(
            new FakeUserRepository { UserById = user },
            new FakeUserRegistrationRepository { RegistrationByUserId = registration },
            new FakeLearnerProfileRepository(),
            new FakeUnitOfWork());

        var result = await handler.HandleAsync(ValidCommand(user.Id));

        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(ProfileApplicationErrors.ProfileAlreadyExist);
    }

    private static RegisterLearnerCommand ValidCommand(Guid userId)
        => new(userId, "Joao", "Souza", new DateOnly(2000, 1, 1));
}
