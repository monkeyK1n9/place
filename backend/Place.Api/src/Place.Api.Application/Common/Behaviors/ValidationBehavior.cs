namespace Place.Api.Application.Common.Behaviors;

using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using ErrorOr;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

public class ValidationBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly IValidator<TRequest>? validator;

    public ValidationBehavior(IValidator<TRequest>? validator = null) => this.validator = validator;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (this.validator is null)
        {
            Debug.Assert(next != null, nameof(next) + " != null");
            return await next().ConfigureAwait(true);
        }

        ValidationResult? validationResult = await this.validator
            .ValidateAsync(request, cancellationToken)
            .ConfigureAwait(false);

        if (validationResult.IsValid)
        {
            Debug.Assert(next != null, nameof(next) + " != null");
            return await next().ConfigureAwait(false);
        }

        List<Error> errors = validationResult.Errors.ConvertAll(validationFailure =>
            Error.Validation(validationFailure.PropertyName, validationFailure.ErrorMessage));
        return (dynamic)errors;

    }
}
