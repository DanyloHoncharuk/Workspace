using Microsoft.AspNetCore.Diagnostics;
using Workspace.Application.Common;

namespace Workspace.API.Middleware
{
    public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext context,
        Exception exception,
        CancellationToken cancellationToken = default)
    {
        var error = new Error("Server.InternalError", "An unexpected error occurred.");

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsJsonAsync(new { isSuccess = false, error }, cancellationToken);

        return true;
    }
}
}
