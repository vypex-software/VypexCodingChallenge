namespace Vypex.CodingChallenge.Application.Results;

public class ValidationFailedResult : IResult
{
    public IDictionary<string, string[]> ValidationErrors { get; }

    public ValidationFailedResult(IDictionary<string, string[]> validationErrors)
    {
        ValidationErrors = validationErrors;
    }
}

public class ValidationFailedResult<T> : ValidationFailedResult, IResult<T>
{
    public ValidationFailedResult(IDictionary<string, string[]> validationErrors) : base(validationErrors) { }
}
