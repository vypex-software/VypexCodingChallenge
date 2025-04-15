using Vypex.CodingChallenge.Application.Interfaces;
using Vypex.CodingChallenge.Application.Models;
using Vypex.CodingChallenge.Domain.Interfaces;
using Vypex.CodingChallenge.Domain.Models;

namespace Vypex.CodingChallenge.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<IEnumerable<EmployeeDTO>> GetAllWithLeavesAsync(CancellationToken cancellationToken)
    {
        return await _employeeRepository.GetAllWithTotalLeaveDaysAsync(cancellationToken);
    }

    public async Task<EmployeeLeaveDetailsDTO> GetEmployeeLeaveDetailsAsync(Guid employeeId, CancellationToken cancellationToken)
    {
        var employeeDetails =  await _employeeRepository.GetEmployeeLeaveDetailsAsync(employeeId, cancellationToken);
        
        return employeeDetails is not null
        ? ToServiceModel(employeeDetails)
        : new EmployeeLeaveDetailsDTO
        {
            EmployeeId = employeeId,
            Leaves = Enumerable.Empty<LeaveDTO>()
        };
    }

    private static EmployeeLeaveDetailsDTO ToServiceModel(Employee employee)
    {
        return new EmployeeLeaveDetailsDTO
        {
            EmployeeId = employee.Id,
            Leaves = employee.Leaves.Select(l => new LeaveDTO
            {
                LeaveId = l.LeaveId,
                StartDate = l.StartDate,
                EndDate = l.EndDate,
                EmployeeId = employee.Id
            }).ToList()
        };
    }
}
