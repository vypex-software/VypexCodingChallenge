using Vypex.CodingChallenge.Application.Models;
using Vypex.CodingChallenge.Domain.Models;

namespace Vypex.CodingChallenge.Application.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDTO>> GetAllWithLeavesAsync(CancellationToken cancellationToken);
    Task<EmployeeLeaveDetailsDTO> GetEmployeeLeaveDetailsAsync(Guid employeeId, CancellationToken cancellationToken);
}
