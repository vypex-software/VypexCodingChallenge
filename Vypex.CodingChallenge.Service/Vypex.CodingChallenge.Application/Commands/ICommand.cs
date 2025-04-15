using MediatR;
using Vypex.CodingChallenge.Application.Results;

namespace Vypex.CodingChallenge.Application.Commands;

internal interface ICommand : ICommandBase, IRequest { }

internal interface ICommand<out TResponse> : ICommandBase, IRequest<IResult<TResponse>> { }
public interface ICommandBase { }
