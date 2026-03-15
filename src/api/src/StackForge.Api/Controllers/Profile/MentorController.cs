using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using StackForge.Application.Profile.UseCases.RegisterMentor;
using StackForge.Application.Shared.Results;

namespace StackForge.Api.Controllers.Profile
{
    [ApiController]
    [Route("api/profile/mentor")]
    public sealed class MentorController : ControllerBase
    {
        private readonly RegisterMentorHandler _handler;
        private readonly IValidator<RegisterMentorCommand> _validator;

        public MentorController(RegisterMentorHandler handler, IValidator<RegisterMentorCommand> validator)
        {
            _handler = handler;
            _validator = validator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(RegisterMentorResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterMentorCommand command)
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

            Result<RegisterMentorResponse> result = await _handler.HandleAsync(command);

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
