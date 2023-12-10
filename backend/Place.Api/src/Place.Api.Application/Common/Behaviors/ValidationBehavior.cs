namespace Place.Api.Application.Common.Behaviors;

using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using ErrorOr;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

/// <summary>
/// Pipeline behavior for handling validation of requests.
/// </summary>
/// <typeparam name="TRequest">Type of the request.</typeparam>
/// <typeparam name="TResponse">Type of the response.</typeparam>
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly IValidator<TRequest>? validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationBehavior{TRequest,TResponse}"/> class.
    /// </summary>
    /// <param name="validator">Validator for the request.</param>
    public ValidationBehavior(IValidator<TRequest>? validator = null) => this.validator = validator;

    /// <summary>
    /// Handles the validation of the request and invokes the next handler in the pipeline.
    /// </summary>
    /// <param name="request">The request being processed.</param>
    /// <param name="next">Delegate to invoke the next handler in the pipeline.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response from the next handler or a validation error.</returns>
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
