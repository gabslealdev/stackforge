using Shouldly;
using StackForge.Application.ProfileContext.Errors;
using StackForge.Application.ProfileContext.UseCases.AddStackToMentor;
using StackForge.Application.Tests.Common;

namespace StackForge.Application.Tests.ProfileContext.UseCases;

public sealed class AddStackToMentorHandlerTests
{
    [Fact]
    public async Task HandleAsync_ShouldAddStackToMentor_WhenMentorAndStackExist()
    {
        var mentor = TestData.CreateMentor();
        var stack = TestData.CreateStack("DotNet", "dotnet");
        var mentorRepository = new FakeMentorProfileRepository { MentorByUserId = mentor };
        var unitOfWork = new FakeUnitOfWork();
        var handler = new AddStackToMentorHandler(
            mentorRepository,
            new FakeStackRepository { StackById = stack },
            unitOfWork);

        var result = await handler.HandleAsync(new AddStackToMentorCommand(mentor.UserId, stack.Id));

        result.IsSuccess.ShouldBeTrue();
        result.Value.StackId.ShouldBe(stack.Id);
        result.Value.StackKey.ShouldBe(stack.Key.Value);
        mentor.Stacks.ShouldContain(stack);
        mentorRepository.UpdatedMentors.ShouldBe([mentor]);
        unitOfWork.SaveChangesCount.ShouldBe(1);
    }

    [Fact]
    public async Task HandleAsync_ShouldFail_WhenMentorDoesNotExist()
    {
        var handler = new AddStackToMentorHandler(
            new FakeMentorProfileRepository(),
            new FakeStackRepository { StackById = TestData.CreateStack() },
            new FakeUnitOfWork());

        var result = await handler.HandleAsync(new AddStackToMentorCommand(Guid.NewGuid(), Guid.NewGuid()));

        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(ProfileApplicationErrors.MentorNotFound);
    }

    [Fact]
    public async Task HandleAsync_ShouldFail_WhenStackDoesNotExist()
    {
        var mentor = TestData.CreateMentor();
        var handler = new AddStackToMentorHandler(
            new FakeMentorProfileRepository { MentorByUserId = mentor },
            new FakeStackRepository(),
            new FakeUnitOfWork());

        var result = await handler.HandleAsync(new AddStackToMentorCommand(mentor.UserId, Guid.NewGuid()));

        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(ProfileApplicationErrors.StackNotFound);
    }
}
