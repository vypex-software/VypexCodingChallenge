using Vypex.CodingChallenge.Application.Interfaces;
using Vypex.CodingChallenge.Application.Results;
using Vypex.CodingChallenge.Domain;
using Vypex.CodingChallenge.Domain.Models;

namespace Vypex.CodingChallenge.Application.Queries.GetEmployeesQuery;

public class GetAllEmployeeDetailsQueryHandler : IQueryHandler<GetAllEmployeeDetailsQuery, IEnumerable<EmployeeDTO>>
{
    private readonly IEmployeeService _employeeService;

    public GetAllEmployeeDetailsQueryHandler(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public async Task<IResult<IEnumerable<EmployeeDTO>>> Handle(GetAllEmployeeDetailsQuery query, CancellationToken cancellationToken)
    {
        var employees = await _employeeService.GetAllWithLeavesAsync(cancellationToken);
        return Result<IEnumerable<EmployeeDTO>>.OkResult(employees);
    }
}
