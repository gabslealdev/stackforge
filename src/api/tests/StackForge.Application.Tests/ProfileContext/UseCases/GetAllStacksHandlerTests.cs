using Shouldly;
using StackForge.Application.ProfileContext.UseCases.GetAllStacks;
using StackForge.Application.Tests.Common;

namespace StackForge.Application.Tests.ProfileContext.UseCases;

public sealed class GetAllStacksHandlerTests
{
    [Fact]
    public async Task HandleAsync_ShouldReturnMappedStacks()
    {
        var first = TestData.CreateStack("CSharp", "csharp");
        var second = TestData.CreateStack("DotNet", "dotnet");
        var handler = new GetAllStacksHandler(new FakeStackRepository { Stacks = [first, second] });

        var result = await handler.HandleAsync(new GetAllStacksQuery());

        result.IsSuccess.ShouldBeTrue();
        result.Value.Count.ShouldBe(2);
        result.Value[0].ShouldBe(new GetAllStacksResponse(first.Id, "CSharp", "csharp"));
        result.Value[1].ShouldBe(new GetAllStacksResponse(second.Id, "DotNet", "dotnet"));
    }

    [Fact]
    public async Task HandleAsync_ShouldReturnEmptyList_WhenRepositoryHasNoStacks()
    {
        var handler = new GetAllStacksHandler(new FakeStackRepository());

        var result = await handler.HandleAsync(new GetAllStacksQuery());

        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBeEmpty();
    }
}
