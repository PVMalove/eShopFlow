using Common.Kernel.Abstractions.Messaging;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Common.Kernel.Behaviors;

public sealed class ValidationPipelineBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators,
    ILogger<ValidationPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseCommand
{
    private readonly IEnumerable<IValidator<TRequest>> _validators =
        validators ?? throw new ArgumentNullException(nameof(validators));

    private readonly ILogger<ValidationPipelineBehavior<TRequest, TResponse>> _logger =
        logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _logger.LogDebug("Processing request {RequestType} for validation", typeof(TRequest).Name);
        ValidationFailure[] validationFailures = await ValidateAsync(request, cancellationToken);

        if (validationFailures.Length == 0)
        {
            _logger.LogDebug("Validation successful for request {RequestType}", typeof(TRequest).Name);
            return await next(cancellationToken);
        }

        throw new ValidationException(validationFailures);
    }

    private async Task<ValidationFailure[]> ValidateAsync(TRequest request, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return Array.Empty<ValidationFailure>();
        }

        ValidationContext<TRequest> context = new(request)
            { RootContextData = { ["CancellationToken"] = cancellationToken } };

        try
        {
            ValidationResult[] validationResults = await Task.WhenAll(
                _validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));

            ValidationFailure[] validationFailures = validationResults
                .Where(result => !result.IsValid)
                .SelectMany(result => result.Errors)
                .ToArray();

            return validationFailures;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Validation failed for request {RequestType}", typeof(TRequest).Name);
            throw;
        }
    }
}