using MediatR;
using Microsoft.AspNetCore.Mvc;
using Workspace.API.Base;
using Workspace.Application.Features.Auth.Commands;
using Workspace.Application.Features.Auth.Common;

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

            return result.IsSuccess ? HandleAuthenticationSuccess(result.Data!) : ErrorResponse(result.Error);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var result = await _sender.Send(command);

            return result.IsSuccess ? HandleAuthenticationSuccess(result.Data!) : ErrorResponse(result.Error);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            var authHeader = Request.Headers["Authorization"].ToString();
            var accessToken = authHeader.StartsWith("Bearer ") ? authHeader.Substring("Bearer ".Length) : null;

            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized();
            }

            var command = new RefreshTokenCommand(accessToken, refreshToken);
            var result = await _sender.Send(command);

            if (!result.IsSuccess)
            {
                return Unauthorized();
            }

            return HandleAuthenticationSuccess(result.Data!);
        }

        private IActionResult HandleAuthenticationSuccess(AuthResponse authResponse)
        {
            SetRefreshTokenCookie(authResponse.RefreshToken);

            return Ok(new
            {
                isSuccess = true,
                data = new
                {
                    userId = authResponse.UserId,
                    accessToken = authResponse.AccessToken
                }
            });
        }

        private void SetRefreshTokenCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true, // Prevents JavaScript access to the cookie
                Secure = true, // Ensures the cookie is only sent over HTTPS
                SameSite = SameSiteMode.Strict, // Prevents the cookie from being sent with cross-site requests
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
    }
}