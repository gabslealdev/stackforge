using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using StackForge.Api.Contracts.IdentityContext.LoginUser.Request;
using StackForge.Api.Contracts.IdentityContext.LoginUser.Response;
using StackForge.Application.IdentityContext.UseCases.LoginUser;
using StackForge.Application.Shared.Results;

namespace StackForge.Api.Controllers.IdentityContext
{
    [ApiController]
    [Route("api/identity/login")]
    public sealed class LoginController : ControllerBase
    {
        private readonly LoginUserHandler _handler;
        private readonly IValidator<LoginUserCommand> _validator;

        public LoginController(LoginUserHandler handler, IValidator<LoginUserCommand> validator)
        {
            _handler = handler;
            _validator = validator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(LoginUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginUserRequestDto request)
        {
            var command = new LoginUserCommand(request.Email, request.Password);
            
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

            Result<LoginUserResponse> result = await _handler.HandleAsync(command);

            if (result.IsFailure)
            {
                return BadRequest(new
                {
                    code = result.Error.Code,
                    message = result.Error.Message
                });
            }
            
            var response = new LoginUserResponseDto(result.Value.AccessToken, result.Value.Expiration, result.Value.ProfileType);
            
            return Ok(response);
        }
    }
}
