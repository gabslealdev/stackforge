using Shouldly;
using StackForge.Application.ProfileContext.Errors;
using StackForge.Application.ProfileContext.UseCases.GetCurrentMentor;
using StackForge.Application.Tests.Common;

namespace StackForge.Application.Tests.ProfileContext.UseCases;

public sealed class GetCurrentMentorHandlerTests
{
    [Fact]
    public async Task HandleAsync_ShouldReturnCurrentMentor_WhenMentorExists()
    {
        var mentor = TestData.CreateMentor(bio: "Experienced software mentor.");
        var stack = TestData.CreateStack("DotNet", "dotnet");
        mentor.AddStack(stack);
        var handler = new GetCurrentMentorHandler(new FakeMentorProfileRepository { MentorWithStacksByUserId = mentor });

        var result = await handler.HandleAsync(new GetCurrentMentorQuery(mentor.UserId));

        result.IsSuccess.ShouldBeTrue();
        result.Value.UserId.ShouldBe(mentor.UserId);
        result.Value.FullName.ShouldBe("Maria Silva");
        result.Value.CourseName.ShouldBe("Computer Science");
        result.Value.Institution.ShouldBe("Stack University");
        result.Value.Bio.ShouldBe("Experienced software mentor.");
        result.Value.Availability.ShouldBe("Unavailable");
        result.Value.Stacks.Count.ShouldBe(1);
        result.Value.Stacks.Single().ShouldBe(new MentorStackResponse(stack.Id, "DotNet", "dotnet"));
    }

    [Fact]
    public async Task HandleAsync_ShouldFail_WhenMentorDoesNotExist()
    {
        var handler = new GetCurrentMentorHandler(new FakeMentorProfileRepository());

        var result = await handler.HandleAsync(new GetCurrentMentorQuery(Guid.NewGuid()));

        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(ProfileApplicationErrors.MentorNotFound);
    }
}
