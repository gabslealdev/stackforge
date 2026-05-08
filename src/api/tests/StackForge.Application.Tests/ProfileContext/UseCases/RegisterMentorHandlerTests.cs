using Shouldly;
using StackForge.Application.ProfileContext.Errors;
using StackForge.Application.ProfileContext.UseCases.RegisterMentor;
using StackForge.Application.Tests.Common;
using StackForge.Domain.IdentityContext.Enums;

namespace StackForge.Application.Tests.ProfileContext.UseCases;

public sealed class RegisterMentorHandlerTests
{
    [Fact]
    public async Task HandleAsync_ShouldRegisterMentor_WhenRegistrationIsValid()
    {
        var user = TestData.CreateUser();
        var registration = TestData.CreateRegistration(user.Id, ProfileType.Mentor);
        var mentorRepository = new FakeMentorProfileRepository();
        var registrationRepository = new FakeUserRegistrationRepository { RegistrationByUserId = registration };
        var unitOfWork = new FakeUnitOfWork();
        var handler = new RegisterMentorHandler(
            new FakeUserRepository { UserById = user },
            registrationRepository,
            mentorRepository,
            unitOfWork);

        var result = await handler.HandleAsync(new RegisterMentorCommand(
            user.Id,
            "Maria",
            "Silva",
            new DateOnly(1990, 1, 1),
            "Computer Science",
            "Stack University",
            1,
            new DateOnly(2020, 1, 1),
            "Experienced software mentor."));

        result.IsSuccess.ShouldBeTrue();
        result.Value.MentorId.ShouldNotBe(Guid.Empty);
        mentorRepository.AddedMentors.Count.ShouldBe(1);
        mentorRepository.AddedMentors[0].Id.ShouldBe(result.Value.MentorId);
        registration.Status.ShouldBe(RegistrationStatus.Completed);
        registrationRepository.UpdatedRegistrations.ShouldBe([registration]);
        unitOfWork.SaveChangesCount.ShouldBe(1);
    }

    [Fact]
    public async Task HandleAsync_ShouldFail_WhenUserDoesNotExist()
    {
        var handler = new RegisterMentorHandler(
            new FakeUserRepository(),
            new FakeUserRegistrationRepository(),
            new FakeMentorProfileRepository(),
            new FakeUnitOfWork());

        var result = await handler.HandleAsync(ValidCommand(Guid.NewGuid()));

        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(ProfileApplicationErrors.UserNotFound);
    }

    [Fact]
    public async Task HandleAsync_ShouldFail_WhenRegistrationDoesNotExist()
    {
        var user = TestData.CreateUser();
        var handler = new RegisterMentorHandler(
            new FakeUserRepository { UserById = user },
            new FakeUserRegistrationRepository(),
            new FakeMentorProfileRepository(),
            new FakeUnitOfWork());

        var result = await handler.HandleAsync(ValidCommand(user.Id));

        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(ProfileApplicationErrors.UserRegistrationNotFound);
    }

    [Fact]
    public async Task HandleAsync_ShouldFail_WhenSelectedProfileIsNotMentor()
    {
        var user = TestData.CreateUser();
        var registration = TestData.CreateRegistration(user.Id, ProfileType.Learner);
        var handler = new RegisterMentorHandler(
            new FakeUserRepository { UserById = user },
            new FakeUserRegistrationRepository { RegistrationByUserId = registration },
            new FakeMentorProfileRepository(),
            new FakeUnitOfWork());

        var result = await handler.HandleAsync(ValidCommand(user.Id));

        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(ProfileApplicationErrors.ProfileInvalid);
    }

    [Fact]
    public async Task HandleAsync_ShouldFail_WhenRegistrationIsCompleted()
    {
        var user = TestData.CreateUser();
        var registration = TestData.CreateRegistration(user.Id, ProfileType.Mentor, completed: true);
        var handler = new RegisterMentorHandler(
            new FakeUserRepository { UserById = user },
            new FakeUserRegistrationRepository { RegistrationByUserId = registration },
            new FakeMentorProfileRepository(),
            new FakeUnitOfWork());

        var result = await handler.HandleAsync(ValidCommand(user.Id));

        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(ProfileApplicationErrors.ProfileAlreadyExist);
    }

    private static RegisterMentorCommand ValidCommand(Guid userId)
        => new(
            userId,
            "Maria",
            "Silva",
            new DateOnly(1990, 1, 1),
            "Computer Science",
            "Stack University",
            1,
            new DateOnly(2020, 1, 1),
            "Experienced software mentor.");
}
