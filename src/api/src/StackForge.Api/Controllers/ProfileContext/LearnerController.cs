using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using StackForge.Application.Abstractions.Messaging;
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
        public async Task<IActionResult> Register([FromBody] RegisterLearnerCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(error => new
                {
                    property = error.PropertyName,
                    message = error.ErrorMessage
                });

                return BadRequest(errors);
            }

            Result<RegisterLearnerResponse> result = await _mediator.SendAsync(command, cancellationToken);

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
    }
}
