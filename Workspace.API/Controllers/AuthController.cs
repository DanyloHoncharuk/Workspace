using MediatR;
using Microsoft.AspNetCore.Mvc;
using Workspace.API.Base;
using Workspace.Application.Features.Auth.Commands.Register;
using Workspace.Application.Features.Auth.Commands.Login;

namespace Workspace.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ApiController
    {
        private readonly ISender _sender;

        public AuthController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            var result = await _sender.Send(command);

            return result.IsSuccess ? Ok(result) : ErrorResponse(result.Error);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var result = await _sender.Send(command);

            return result.IsSuccess ? Ok(result) : ErrorResponse(result.Error);
        }
    }
}