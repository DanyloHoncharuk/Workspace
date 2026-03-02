using Microsoft.AspNetCore.Mvc;
using Workspace.Application.Common;

namespace Workspace.API.Base
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiController : ControllerBase
    {
        protected IActionResult ErrorResponse(Error? error)
        {
            if (error is null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError, 
                    new { isSuccess = false, error = new Error("UnknownError", "An unknown error occurred.") });
            }
            var statusCode = error.Type switch
            {
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };
            return StatusCode(statusCode, new { isSuccess = false, error });
        }
    }
}