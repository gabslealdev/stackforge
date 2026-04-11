using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackForge.Api.Contracts.Profile.MentorProfile.Requests;
using StackForge.Application.Profile.UseCases.AddStackToMentor;
using StackForge.Application.Profile.UseCases.RegisterMentor;
using StackForge.Application.Profile.UseCases.UpdateMentorAvailability;
using StackForge.Application.Shared.Results;
using System.Security.Claims;

namespace StackForge.Api.Controllers.Profile
{
    [ApiController]
    [Route("api/profile/mentor")]
    public sealed class MentorController : ControllerBase
    {
        private readonly RegisterMentorHandler _registerMentorHandler;
        private readonly IValidator<RegisterMentorCommand> _registerMentorValidator;
        private readonly AddStackToMentorHandler _addStackToMentorHandler;
        private readonly IValidator<AddStackToMentorCommand> _addStackToMentorValidator;
        private readonly UpdateMentorAvailabilityHandler _updateMentorAvailability;

        public MentorController(RegisterMentorHandler registerMentorHandler, 
            IValidator<RegisterMentorCommand> registerMentorValidator, 
            IValidator<AddStackToMentorCommand> addStackToMentorValidator,
            AddStackToMentorHandler addStackToMentorHandler,
            UpdateMentorAvailabilityHandler updateMentorAvailability)
        {
            _registerMentorHandler = registerMentorHandler;
            _registerMentorValidator = registerMentorValidator;
            _addStackToMentorValidator = addStackToMentorValidator;
            _addStackToMentorHandler = addStackToMentorHandler;
            _updateMentorAvailability = updateMentorAvailability;
        }

        [HttpPost]
        [ProducesResponseType(typeof(RegisterMentorResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterMentorCommand command)
        {
            var validationResult = await _registerMentorValidator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(error => new 
                {
                    property = error.PropertyName,
                    message = error.ErrorMessage
                });

                return BadRequest(errors);
            }

            Result<RegisterMentorResponse> result = await _registerMentorHandler.HandleAsync(command);

            if (result.IsFailure)
            {
                return BadRequest(new
                {
                    result.Error.Code,
                    result.Error.Message
                });
            }

            return Created(string.Empty, result.Value);
        }

        [Authorize(Policy = "MentorOnly")]
        [HttpPost("stacks")]
        [ProducesResponseType(typeof(AddStackToMentorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddStack([FromBody] AddStackToMentorRequest request)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
             
            if (!Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            var command = new AddStackToMentorCommand(userId, request.StackId);

            var validationResult = await _addStackToMentorValidator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(error => new
                {
                    property = error.PropertyName,
                    message = error.ErrorMessage
                });

                return BadRequest(errors);
            }

            Result<AddStackToMentorResponse> result = await _addStackToMentorHandler.HandleAsync(command);

            if (result.IsFailure)
            {
                return BadRequest(new
                {
                    result.Error.Code,
                    result.Error.Message
                });
            }

            return Ok(result.Value);
        }

        [Authorize(Policy = "MentorOnly")]
        [HttpPatch("availability")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAvailability([FromBody] UpdateMentorAvailabilityRequest request)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            var command = new UpdateMentorAvailabilityCommand(userId, request.IsAvailable);

            await _updateMentorAvailability.HandleAsync(command);

            return NoContent();

        }

    }
}
