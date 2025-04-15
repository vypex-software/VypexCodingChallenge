namespace Vypex.CodingChallenge.Application.Results;
public class UnexpectedErrorResult : IResult
{
    public string Error { get; }

    public UnexpectedErrorResult(string error)
    {
        Error = error;
    }
}

public class UnexpectedErrorResult<T> : UnexpectedErrorResult, IResult<T>
{
    public UnexpectedErrorResult(string error) : base(error) { }
}
