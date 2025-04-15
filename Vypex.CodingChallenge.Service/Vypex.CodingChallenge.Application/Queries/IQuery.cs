using MediatR;
using Vypex.CodingChallenge.Application.Results;

namespace Vypex.CodingChallenge.Application.Queries;

internal interface IQuery<out TResponse> : IRequest<IResult<TResponse>> { }
internal interface ICachedQuery<out TResponse> : IQuery<TResponse> { }
