using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Vypex.CodingChallenge.Application.Results;

namespace Vypex.CodingChallenge.Application.PipelineBehavior;
public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
where TRequest : IRequest<TResponse>
where TResponse : IResult
{
    private readonly IValidator<TRequest>? _validator;

    public ValidationPipelineBehavior(IValidator<TRequest>? validator = null) => _validator = validator;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validator is null)
            return await next();

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return (TResponse)FormatValidationResult(validationResult);

        return await next();
    }

    private static IResult FormatValidationResult(ValidationResult validationResult)
    {
        var responseType = typeof(TResponse);
        if (responseType.IsGenericType)
        {
            var genericType = responseType.GenericTypeArguments[0];
            var genericValidationFailedResultType = typeof(ValidationFailedResult<>).MakeGenericType(genericType);
            var result = Activator.CreateInstance(genericValidationFailedResultType, validationResult.ToDictionary()) ?? throw new ValidationException(validationResult.Errors);

            return (IResult)result;
        }

        return Result.ValidationFailedResult(validationResult.ToDictionary());
    }
}
