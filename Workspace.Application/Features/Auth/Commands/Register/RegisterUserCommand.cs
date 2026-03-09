using MediatR;
using Workspace.Application.Common;
using Workspace.Application.Features.Auth.Common;

namespace Workspace.Application.Features.Auth.Commands
{
    public record RegisterUserCommand(
        string Login, 
        string Password,
        string Name,
        string Surname,
        string? Email
        ) : IRequest<Result<AuthResponse>>;
}