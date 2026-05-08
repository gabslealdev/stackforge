using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using StackForge.Application.ProfileContext.UseCases.RegisterLearner;
using StackForge.Application.Shared.Results;

namespace StackForge.Api.Controllers.ProfileContext
{
    [ApiController]
    [Route("api/profile/learner")]
    public sealed class LearnerController : ControllerBase
    {
        private readonly RegisterLearnerHandler _handler;
        private readonly IValidator<RegisterLearnerCommand> _validator;

        public LearnerController(RegisterLearnerHandler handler, IValidator<RegisterLearnerCommand> validator)
        {
            _handler = handler;
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

            Result<RegisterLearnerResponse> result = await _handler.HandleAsync(command);

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
