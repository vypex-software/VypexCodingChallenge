using MediatR;
using Vypex.CodingChallenge.Application.Results;

namespace Vypex.CodingChallenge.Application.Queries;

internal interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, IResult<TResponse>>
where TQuery : IQuery<TResponse>
{
}
