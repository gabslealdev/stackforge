using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackForge.Api.Contracts.ProfileContext.MentorProfile.AddStackToMentor.Request;
using StackForge.Api.Contracts.ProfileContext.MentorProfile.AddStackToMentor.Response;
using StackForge.Api.Contracts.ProfileContext.MentorProfile.RegisterMentor.Requests;
using StackForge.Api.Contracts.ProfileContext.MentorProfile.RegisterMentor.Responses;
using StackForge.Api.Contracts.ProfileContext.MentorProfile.UpdateMentorAvailability.Request;
using StackForge.Application.Abstractions.Messaging;
using StackForge.Application.ProfileContext.UseCases.AddStackToMentor;
using StackForge.Application.ProfileContext.UseCases.GetCurrentMentor;
using StackForge.Application.ProfileContext.UseCases.RegisterMentor;
using StackForge.Application.ProfileContext.UseCases.UpdateMentorAvailability;
using StackForge.Application.Shared.Results;

namespace StackForge.Api.Controllers.ProfileContext
{
    [ApiController]
    [Route("api/profile/mentor")]
    public sealed class MentorController : ControllerBase
    {
        private readonly IMediator _mediator; 
        private readonly IValidator<RegisterMentorCommand> _registerMentorValidator;
        private readonly IValidator<AddStackToMentorCommand> _addStackToMentorValidator;

        public MentorController( 
            IMediator mediator,
            IValidator<RegisterMentorCommand> registerMentorValidator, 
            IValidator<AddStackToMentorCommand> addStackToMentorValidator
            )
        {
            _mediator = mediator;
            _registerMentorValidator = registerMentorValidator;
            _addStackToMentorValidator = addStackToMentorValidator;

        }

        [HttpPost]
        [ProducesResponseType(typeof(RegisterMentorResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterMentorRequestDto request, CancellationToken cancellationToken)
        {
            if(!Guid.TryParse(request.UserId, out var userId))
                return BadRequest();


            var command = new RegisterMentorCommand(
                userId,
                request.FirstName,
                request.LastName,
                request.BirthDate,
                request.CourseName,
                request.Institution,
                request.EducationStatus,
                request.ConclusionDate,
                request.Bio
            );
            var validationResult = await _registerMentorValidator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(error => new 
                {
                    property = error.PropertyName,
                    message = error.ErrorMessage
                });

                return BadRequest(errors);
            }

            Result<RegisterMentorResponse> result = await _mediator.SendAsync(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(new
                {
                    result.Error.Code,
                    result.Error.Message
                });
            }

            var response = new RegisterMentorResponseDto(result.Value.MentorId);

            return Created(string.Empty, result.Value);
        }

        [Authorize(Policy = "MentorOnly")]
        [HttpPost("stacks")]
        [ProducesResponseType(typeof(AddStackToMentorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddStack([FromBody] AddStackToMentorRequestDto request, CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
             
            if (!Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            var command = new AddStackToMentorCommand(userId, request.StackId);

            var validationResult = await _addStackToMentorValidator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(error => new
                {
                    property = error.PropertyName,
                    message = error.ErrorMessage
                });

                return BadRequest(errors);
            }

            Result<AddStackToMentorResponse> result = await _mediator.SendAsync(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(new
                {
                    result.Error.Code,
                    result.Error.Message
                });
            }
            
            var response = new AddStackToMentorResponseDto(result.Value.StackId, result.Value.StackKey);

            return Ok(response);
        }

        [Authorize(Policy = "MentorOnly")]
        [HttpPatch("availability")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAvailability([FromBody] UpdateMentorAvailabilityRequestDto request, CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            var command = new UpdateMentorAvailabilityCommand(userId, request.IsAvailable);

            await _mediator.SendAsync(command, cancellationToken);

            return NoContent();

        }

        [Authorize(Policy = "MentorOnly")]
        [HttpGet("me")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCurrentMentor(CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            var query = new GetCurrentMentorQuery(userId);

            Result<GetCurrentMentorResponse> result = await _mediator.SendAsync(query, cancellationToken);

            if(result.IsFailure)
            {
                return BadRequest(new
                {
                    result.Error.Code,
                    result.Error.Message
                });
            }

            return Ok(result.Value);
        }


    }
}
