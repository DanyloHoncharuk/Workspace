using MediatR;
using Workspace.Application.Common;
using Workspace.Application.Common.Errors;
using Workspace.Application.Features.Auth.Common;
using Workspace.Application.Interfaces;
using Workspace.Domain.Entities;

namespace Workspace.Application.Features.Auth.Commands
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<AuthResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtGenerator _jwtGenerator;

        public RegisterUserCommandHandler(
            IUserRepository userRepository, 
            IPasswordHasher passwordHasher, 
            IUnitOfWork unitOfWork, 
            IJwtGenerator jwtGenerator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<Result<AuthResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if(await _userRepository.GetUserByLoginAsync(request.Login) is not null)
                return Errors.User.UserAlreadyExists;
            
            User user = new User(
                request.Login,
                _passwordHasher.HashPassword(request.Password),
                request.Name,
                request.Surname,
                request.Email
            );

            _userRepository.AddUser(user);

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