using Vypex.CodingChallenge.Application.Interfaces;
using Vypex.CodingChallenge.Application.Results;
using Vypex.CodingChallenge.Domain.Exceptions;
using Vypex.CodingChallenge.Domain.Interfaces;

namespace Vypex.CodingChallenge.Application.Commands.AddOrUpdateLeaveCommand;

internal class UpsertEmployeeLeaveCommandHandler : ICommandHandler<UpsertEmployeeLeaveCommand, bool>
{
    private readonly ILeaveService _leaveService;
    private readonly IUnitOfWork _unitOfWork;
    public UpsertEmployeeLeaveCommandHandler(ILeaveService leaveService, IUnitOfWork unitOfWork)
    {
        _leaveService = leaveService;
        _unitOfWork = unitOfWork;
    }

    public async Task<IResult<bool>> Handle(UpsertEmployeeLeaveCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _leaveService.UpsertLeavesAsync(command.employeeId, command.leaves, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<bool>.OkResult(result);
        }
        catch (DomainValidationException ex)
        {
            return Result<bool>.UnprocessableResult("Validation failed: " + ex.Message);
        }
        catch (Exception ex)
        {
            return Result<bool>.UnprocessableResult("An unexpected error occurred while updating leave: " + ex.Message);
        }
    }
}
