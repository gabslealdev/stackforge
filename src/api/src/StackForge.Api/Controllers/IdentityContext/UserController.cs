using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using StackForge.Api.Contracts.IdentityContext.RegisterUser.Request;
using StackForge.Api.Contracts.IdentityContext.RegisterUser.Response;
using StackForge.Application.Abstractions.Messaging;
using StackForge.Application.IdentityContext.UseCases.RegisterUser;
using StackForge.Application.Shared.Results;
using StackForge.Domain.IdentityContext.Enums;

namespace StackForge.Api.Controllers.IdentityContext
{
    [ApiController]
    [Route("api/identity/user")]
    public sealed class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<RegisterUserCommand> _validator;

        public UserController(IMediator mediator, IValidator<RegisterUserCommand> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(RegisterUserResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequestDto request)
        {
            if (!Enum.TryParse<ProfileType>(request.SelectedProfileType, true, out ProfileType profileType))
                return BadRequest("Invalid profile type");
            
            var command = new RegisterUserCommand(request.Email, request.Password, profileType);
            
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

            Result<RegisterUserResponse> result = await _mediator.SendAsync(command);

            if (result.IsFailure)
            {
                return BadRequest(new { 
                    code = result.Error.Code,
                    message = result.Error.Message
                });
            }
            
            var response = new RegisterUserResponseDto(result.Value.UserId.ToString());

            return Created(string.Empty, response);
        }
    }
}
