namespace Workspace.Application.Features.Auth.Common
{
    public record AuthResponse(Guid UserId, string Token);
}