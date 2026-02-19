using FluentValidation;
using Workspace.Application.Common;
using Workspace.Application.Common.Errors;
using Workspace.Application.Common.Validation;

namespace Workspace.Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty()
                .MinimumLength(3).WithError(Errors.User.LoginLength)
                .MaximumLength(50).WithError(Errors.User.LoginLength);
            
            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8).WithError(Errors.User.PasswordLength)
                .MaximumLength(30).WithError(Errors.User.PasswordLength);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100).WithError(Errors.User.NameLength)
                .Matches(RegexConstants.LatinName).WithError(Errors.User.InvalidNameFormat);
            
            RuleFor(x => x.Surname)
                .NotEmpty()
                .MaximumLength(100).WithError(Errors.User.SurnameLength)
                .Matches(RegexConstants.LatinName).WithError(Errors.User.InvalidSurnameFormat);

            RuleFor(x => x.Email)
                .EmailAddress().WithError(Errors.User.InvalidEmailFormat)
                .When(x => !string.IsNullOrEmpty(x.Email));
            
        }
    }
}