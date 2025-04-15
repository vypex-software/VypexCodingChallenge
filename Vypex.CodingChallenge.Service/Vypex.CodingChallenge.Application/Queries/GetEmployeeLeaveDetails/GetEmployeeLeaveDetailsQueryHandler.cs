using Vypex.CodingChallenge.Application.Interfaces;
using Vypex.CodingChallenge.Application.Models;
using Vypex.CodingChallenge.Application.Results;

namespace Vypex.CodingChallenge.Application.Queries.GetEmployeeLeaveDetails;
public class GetEmployeeLeaveDetailsQueryHandler : IQueryHandler<GetEmployeeLeaveDetailsQuery, EmployeeLeaveDetailsDTO>
{
    private readonly IEmployeeService _employeeService;

    public GetEmployeeLeaveDetailsQueryHandler(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public async Task<IResult<EmployeeLeaveDetailsDTO>> Handle(GetEmployeeLeaveDetailsQuery query, CancellationToken cancellationToken)
    {
        var employeeDetails = await _employeeService.GetEmployeeLeaveDetailsAsync(query.EmployeeId, cancellationToken);
        return Result<EmployeeLeaveDetailsDTO>.OkResult(employeeDetails);
    }
}
