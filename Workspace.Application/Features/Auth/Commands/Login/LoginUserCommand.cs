using MediatR;
using Workspace.Application.Common;
using Workspace.Application.Features.Auth.Common;

namespace Workspace.Application.Features.Auth.Commands.Login
{
    public record LoginUserCommand(string Login, string Password) : IRequest<Result<AuthResponse>>;
}