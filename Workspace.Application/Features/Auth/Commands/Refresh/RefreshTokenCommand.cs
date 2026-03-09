using MediatR;
using Workspace.Application.Common;
using Workspace.Application.Features.Auth.Common;

namespace Workspace.Application.Features.Auth.Commands
{
    public record RefreshTokenCommand(string AccessToken, string RefreshToken) : IRequest<Result<AuthResponse>>;
}
