using MediatR;
using Workspace.Application.Common;
using Workspace.Application.Common.Errors;
using Workspace.Application.Features.Auth.Common;
using Workspace.Application.Interfaces;

namespace Workspace.Application.Features.Auth.Commands.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<AuthResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtGenerator _jwtGenerator;

        private readonly IPasswordHasher _passwordHasher;

        public LoginUserCommandHandler(IUserRepository userRepository, IJwtGenerator jwtGenerator, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _jwtGenerator = jwtGenerator;
            _passwordHasher = passwordHasher;
        }


        public async Task<Result<AuthResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByLoginAsync(request.Login);

            if (user is null || !_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
                return Errors.User.InvalidCredentials;

            var token = _jwtGenerator.GenerateToken(user.Id.ToString(), user.Login);

            return new AuthResponse(user.Id, token);
        }
    }
}