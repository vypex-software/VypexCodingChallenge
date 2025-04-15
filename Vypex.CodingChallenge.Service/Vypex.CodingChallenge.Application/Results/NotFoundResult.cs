namespace Vypex.CodingChallenge.Application.Results;

public class NotFoundResult : IResult
{
    public string Error { get; }

    public NotFoundResult(string error)
    {
        Error = error;
    }
}
public class NotFoundResult<T> : NotFoundResult, IResult<T>
{
    public NotFoundResult(string error) : base(error) { }
}

