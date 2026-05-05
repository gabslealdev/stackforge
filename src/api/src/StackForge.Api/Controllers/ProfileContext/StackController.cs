using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackForge.Api.Contracts.ProfileContext.MentorProfile.AddStackToMentor.Response;
using StackForge.Application.Profile.UseCases.GetAllStacks;

namespace StackForge.Api.Controllers.ProfileContext
{
    [ApiController]
    [Route("api/stacks")]
    [Authorize(Policy = "MentorOnly")]
    public sealed class StackController : ControllerBase
    {
        private readonly GetAllStacksHandler _handler;

        public StackController(GetAllStacksHandler handler)
        {
            _handler = handler;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<GetAllStacksResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllStacksQuery();

            var result = await _handler.HandleAsync(query);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);

        }

    }
}
