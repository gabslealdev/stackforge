using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackForge.Api.Contracts.MentorshipContext.SearchMentorByStacks;
using StackForge.Api.Contracts.MentorshipContext.SearchMentorByStacks.Response;
using StackForge.Api.Contracts.MentorshipContext.SearchStack.Response;
using StackForge.Application.Abstractions.Messaging;
using StackForge.Application.MentorshipContext.UseCases.SearchMentorByStacks;
using StackForge.Application.MentorshipContext.UseCases.SearchStack;
using StackForge.Application.Shared.Results;

namespace StackForge.Api.Controllers.MentorshipContext;

[ApiController]
[Route("/mentorship/search")]
[Authorize(Policy = "LearnerOnly")]
public sealed partial class MentorshipController : ControllerBase
{
    private readonly IMediator _mediator;

    public MentorshipController(IMediator  mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("mentor")]
    [ProducesResponseType(typeof(IReadOnlyList<SearchMentorByStacksResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SearchMentorByStacks([FromBody] SearchMentorByStacksRequestDto request)
    {
        var query = new SearchMentorByStacksQuery(request.StackIds);
        
        Result<IReadOnlyList<SearchMentorByStacksResponse>> result = await _mediator.SendAsync(query);

        if (result.IsFailure)
            return BadRequest(result.Error);

        var response = result.Value.Select(mentor => new SearchMentorByStacksResponseDto(
            mentor.MentorId,
            mentor.FullName,
            mentor.CourseName,
            mentor.Institution,
            mentor.Stacks
                .Select(stack => new MentorStackResponseDto(
                    stack.StackId,
                    stack.Name,
                    stack.Key))
                .ToList()
        )).ToList();
        
        return Ok(response);
    }

    [HttpPost("stack")]
    [ProducesResponseType(typeof(IReadOnlyList<SearchMentorByStacksResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SearchStack([FromBody] SearchStackQuery request)
    {
        var query = new SearchStackQuery(request.SearchTerm);
        
        var result = await _mediator.SendAsync(query);
        
        if (result.IsFailure)
            return BadRequest(result.Error);

        var response = result.Value.Select(stack => new SearchStackResponseDto(
            stack.StackId, 
            stack.Name, stack.Key
            )).ToList();
        
        return Ok(response);
    }
    
    
}