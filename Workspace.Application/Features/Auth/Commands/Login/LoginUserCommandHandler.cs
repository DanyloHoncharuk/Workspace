using MediatR;
using Workspace.Application.Common;
using Workspace.Application.Common.Errors;
using Workspace.Application.Features.Auth.Common;
using Workspace.Application.Interfaces;
using Workspace.Domain.Entities;

namespace Workspace.Application.Features.Auth.Commands
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<AuthResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IPasswordHasher _passwordHasher;

        public LoginUserCommandHandler(
            IUserRepository userRepository, 
            IJwtGenerator jwtGenerator, 
            IPasswordHasher passwordHasher,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _jwtGenerator = jwtGenerator;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
        }


        public async Task<Result<AuthResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByLoginAsync(request.Login);

            if (user is null || !_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
                return Errors.User.InvalidCredentials;

            var accessToken = _jwtGenerator.GenerateToken(user.Id.ToString(), user.Login);
            var refreshTokenString = _jwtGenerator.GenerateRefreshToken();

            var refreshToken = new RefreshToken(
                user.Id,
                refreshTokenString,
                DateTime.UtcNow.AddDays(7)
            );

            _userRepository.AddRefreshToken(refreshToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new AuthResponse(user.Id, accessToken, refreshTokenString);
        }
    }
}