using FluentValidation;
using MediatR;

namespace Workspace.Application.Common.Validation
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(
            TRequest request, 
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (!_validators.Any()) return await next();

            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

            if (failures.Any())
            {
                var firstFailure = failures.First();
                var error = new Error(firstFailure.ErrorCode, firstFailure.ErrorMessage);

                var failureMethod = typeof(TResponse).GetMethod("Failure");

                if (failureMethod == null)
                {
                    throw new InvalidOperationException($"The {typeof(TResponse).Name} type does not contain a Failure method.");
                }
                
                var result = failureMethod.Invoke(null, new object[] { error });

                if (result is TResponse validatedResult)
                {
                    return validatedResult;
                }
                
                throw new InvalidOperationException($"The {failureMethod.Name} method of {typeof(TResponse).Name} returned null.");
            }

            return await next();
        }
    }
}