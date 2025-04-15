namespace Vypex.CodingChallenge.Application.Results;

public class SuccessfulResult : IResult
{
    public SuccessfulResultType Type { get; }


    public SuccessfulResult(SuccessfulResultType type)
    {
        Type = type;
    }
}

public class SuccessfulResult<T> : SuccessfulResult, IResult<T>
{
    public T Content { get; }

    public SuccessfulResult(T content, SuccessfulResultType type) : base(type)
    {
        Content = content;
    }
}
