namespace Vypex.CodingChallenge.Application.Results;
public class UnprocessableResult : IResult
{
    public string Error { get; }

    public UnprocessableResult(string error)
    {
        Error = error;
    }
}
public class UnprocessableResult<T> : UnprocessableResult, IResult<T>
{
    public UnprocessableResult(string error) : base(error) { }
}
