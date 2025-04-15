namespace Vypex.CodingChallenge.Application.Results;

public static class Result
{
    public static SuccessfulResult OkResult() => new(SuccessfulResultType.Ok);

    public static SuccessfulResult CreatedResult() => new(SuccessfulResultType.Created);

    public static ValidationFailedResult ValidationFailedResult(IDictionary<string, string[]> errors) => new(errors);

    public static NotFoundResult NotFoundResult(string error) => new(error);

    public static UnexpectedErrorResult UnexpectedErrorResult(string error) => new(error);

    public static UnprocessableResult UnprocessableResult(string error) => new(error);
}

public static class Result<T>
{
    public static SuccessfulResult<T> OkResult(T content) => new(content, SuccessfulResultType.Ok);

    public static SuccessfulResult<T> CreatedResult(T content) => new(content, SuccessfulResultType.Created);

    public static ValidationFailedResult<T> ValidationFailedResult(IDictionary<string, string[]> errors) => new(errors);

    public static NotFoundResult<T> NotFoundResult(string error) => new(error);

    public static UnexpectedErrorResult<T> UnexpectedErrorResult(string error) => new(error);

    public static UnprocessableResult<T> UnprocessableResult(string error) => new(error);
}
