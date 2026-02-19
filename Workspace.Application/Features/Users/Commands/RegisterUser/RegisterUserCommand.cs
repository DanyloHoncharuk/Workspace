using MediatR;
using Workspace.Application.Common;

namespace Workspace.Application.Features.Users.Commands.RegisterUser
{
    public record RegisterUserCommand(
        string Login, 
        string Password,
        string Name,
        string Surname,
        string? Email
        ) : IRequest<Result<Guid>>;
}