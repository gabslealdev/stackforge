using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackForge.Application.Abstractions.Messaging;
using StackForge.Application.ProfileContext.UseCases.GetCurrentLearner;
using StackForge.Application.ProfileContext.UseCases.RegisterLearner;
using StackForge.Application.Shared.Results;

namespace StackForge.Api.Controllers.ProfileContext
{
    [ApiController]
    [Route("api/profile/learner")]
    public sealed class LearnerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<RegisterLearnerCommand> _validator;

        public LearnerController(IMediator mediator, IValidator<RegisterLearnerCommand> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(RegisterLearnerResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterLearnerCommand command)
        {
            var validationResult = await _validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(error => new
                {
                    property = error.PropertyName,
                    message = error.ErrorMessage
                });

                return BadRequest(errors);
            }

            Result<RegisterLearnerResponse> result = await _mediator.SendAsync(command);

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

        [Authorize(Policy = "LearnerOnly")]
        [HttpGet("me")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCurrentLearner()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (!Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            var query = new GetCurrentLearnerQuery(userId);
            
            Result<GetCurrentLearnerResponse> result = await _mediator.SendAsync(query);

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
        
    }
}
