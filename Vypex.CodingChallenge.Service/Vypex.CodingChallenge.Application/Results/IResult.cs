namespace Vypex.CodingChallenge.Application.Results;

public interface IResult { }

public interface IResult<out T> : IResult { }
