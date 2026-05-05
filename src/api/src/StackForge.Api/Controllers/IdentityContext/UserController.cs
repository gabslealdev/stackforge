using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using StackForge.Application.Identity.UseCases.RegisterUser;
using StackForge.Application.Shared.Results;

namespace StackForge.Api.Controllers.Identity
{
    [ApiController]
    [Route("api/identity/user")]
    public sealed class UserController : ControllerBase
    {
        private readonly RegisterUserHandler _handler;
        private readonly IValidator<RegisterUserCommand> _validator;

        public UserController(RegisterUserHandler handler, IValidator<RegisterUserCommand> validator)
        {
            _handler = handler;
            _validator = validator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(RegisterUserResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
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

            Result<RegisterUserResponse> result = await _handler.HandleAsync(command);

            if (result.IsFailure)
            {
                return BadRequest(new { 
                    code = result.Error.Code,
                    message = result.Error.Message
                });
            }

            return Created(string.Empty, result.Value);
        }
    }
}
