using MediatR;
using Workspace.Application.Common;
using Workspace.Application.Common.Errors;
using Workspace.Application.Features.Auth.Common;
using Workspace.Application.Interfaces;
using Workspace.Domain.Entities;

namespace Workspace.Application.Features.Auth.Commands.Register
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

            _userRepository.CreateUser(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var token = _jwtGenerator.GenerateToken(user.Id.ToString(), user.Login);

            return new AuthResponse(user.Id, token);
        }
    }
}