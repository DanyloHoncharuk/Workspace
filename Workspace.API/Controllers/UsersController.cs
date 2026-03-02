using MediatR;
using Microsoft.AspNetCore.Mvc;
using Workspace.API.Base;
using Workspace.Application.Features.Users.Commands.RegisterUser;

namespace Workspace.API.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ApiController
    {
        private readonly ISender _sender;

        public UsersController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            var result = await _sender.Send(command);

            return result.IsSuccess ? Ok(result) : ErrorResponse(result.Error);
        }
    }
}