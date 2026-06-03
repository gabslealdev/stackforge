using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackForge.Api.Contracts.MentorshipContext.SearchMentorByStacks;
using StackForge.Api.Contracts.MentorshipContext.SearchMentorByStacks.Response;
using StackForge.Api.Contracts.MentorshipContext.SearchStack.Response;
using StackForge.Api.Contracts.MentorshipContext.SendMentorshipRequest;
using StackForge.Application.Abstractions.Messaging;
using StackForge.Application.MentorshipContext.UseCases.SearchMentorByStacks;
using StackForge.Application.MentorshipContext.UseCases.SearchStack;
using StackForge.Application.MentorshipContext.UseCases.SendMentorshipRequest;
using StackForge.Application.Shared.Results;

namespace StackForge.Api.Controllers.MentorshipContext;

[ApiController]
[Route("/mentorship")]
[Authorize(Policy = "LearnerOnly")]
public sealed partial class MentorshipController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<SendMentorshipRequestCommand> _validator;

    public MentorshipController(IMediator  mediator, IValidator<SendMentorshipRequestCommand> validator)
    {
        _mediator = mediator;
        _validator = validator;
    }

    [HttpPost("search/mentor")]
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

    [HttpPost("request")]
    [ProducesResponseType(typeof(SendMentorshipRequestResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SendMentorshipRequest([FromBody] SendMentorshipRequestRequestDto request,
        CancellationToken cancellationToken)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (!Guid.TryParse(userIdClaim, out var userId))
            return Unauthorized();

        var command = new SendMentorshipRequestCommand(userId, request.MentorId, request.StackId, request.Goal);

        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            var errors  = validationResult.Errors.Select(error => new 
            {
                property = error.PropertyName,
                message = error.ErrorMessage
            });
            
            return BadRequest(errors);
        }
        
        Result<SendMentorshipRequestResponse> result = await _mediator.SendAsync(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(new
            {
                code = result.Error.Code,
                message = result.Error.Message
            });
        }

        var response = new SendMentorshipRequestResponseDto(result.Value.MentorshipRequestId);
        
        return Ok(response);
    }
    
}