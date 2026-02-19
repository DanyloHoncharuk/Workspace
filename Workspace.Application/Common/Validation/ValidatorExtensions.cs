using FluentValidation;

namespace Workspace.Application.Common.Validation
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(
            this IRuleBuilderOptions<T, TProperty> rule, Error error)
        {
            return rule
                .WithErrorCode(error.Code)
                .WithMessage(error.Description);
        }
    }
}