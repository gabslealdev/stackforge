using Shouldly;
using StackForge.Application.ProfileContext.Errors;
using StackForge.Application.ProfileContext.UseCases.UpdateMentorAvailability;
using StackForge.Application.Tests.Common;
using StackForge.Domain.ProfileContext.Enums;

namespace StackForge.Application.Tests.ProfileContext.UseCases;

public sealed class UpdateMentorAvailabilityHandlerTests
{
    [Fact]
    public async Task HandleAsync_ShouldMarkMentorAsAvailable_WhenMentorExistsAndHasStack()
    {
        var mentor = TestData.CreateMentor(withStack: true);
        var mentorRepository = new FakeMentorProfileRepository { MentorByUserId = mentor };
        var unitOfWork = new FakeUnitOfWork();
        var handler = new UpdateMentorAvailabilityHandler(mentorRepository, unitOfWork);

        var result = await handler.HandleAsync(new UpdateMentorAvailabilityCommand(mentor.UserId, true));

        result.IsSuccess.ShouldBeTrue();
        mentor.Availability.ShouldBe(AvailabilityStatus.Available);
        mentorRepository.UpdatedMentors.ShouldBe([mentor]);
        unitOfWork.SaveChangesCount.ShouldBe(1);
    }

    [Fact]
    public async Task HandleAsync_ShouldMarkMentorAsUnavailable_WhenMentorExists()
    {
        var mentor = TestData.CreateMentor(withStack: true);
        mentor.MarkAsAvailable();
        var mentorRepository = new FakeMentorProfileRepository { MentorByUserId = mentor };
        var unitOfWork = new FakeUnitOfWork();
        var handler = new UpdateMentorAvailabilityHandler(mentorRepository, unitOfWork);

        var result = await handler.HandleAsync(new UpdateMentorAvailabilityCommand(mentor.UserId, false));

        result.IsSuccess.ShouldBeTrue();
        mentor.Availability.ShouldBe(AvailabilityStatus.Unavailable);
        mentorRepository.UpdatedMentors.ShouldBe([mentor]);
        unitOfWork.SaveChangesCount.ShouldBe(1);
    }

    [Fact]
    public async Task HandleAsync_ShouldFail_WhenMentorDoesNotExist()
    {
        var unitOfWork = new FakeUnitOfWork();
        var mentorRepository = new FakeMentorProfileRepository();
        var handler = new UpdateMentorAvailabilityHandler(mentorRepository, unitOfWork);

        var result = await handler.HandleAsync(new UpdateMentorAvailabilityCommand(Guid.NewGuid(), true));

        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(ProfileApplicationErrors.MentorNotFound);
        mentorRepository.UpdatedMentors.ShouldBeEmpty();
        unitOfWork.SaveChangesCount.ShouldBe(0);
    }
}
