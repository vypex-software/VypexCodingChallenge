using MediatR;
using Vypex.CodingChallenge.Application.Results;

namespace Vypex.CodingChallenge.Application.Commands;

internal interface ICommandHandler<in TCommand> : IRequestHandler<TCommand> where TCommand : ICommand
{
}

internal interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, IResult<TResponse>> where TCommand : ICommand<TResponse>
{
}
