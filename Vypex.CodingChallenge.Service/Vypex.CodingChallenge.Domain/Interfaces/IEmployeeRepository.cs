﻿using Vypex.CodingChallenge.Domain.Models;

namespace Vypex.CodingChallenge.Domain.Interfaces
{
    public interface IEmployeeRepository: IRepository<Employee>
    {
        Task<List<EmployeeDTO>> GetAllWithTotalLeaveDaysAsync(CancellationToken cancellationToken);
        Task<Employee?> GetEmployeeLeaveDetailsAsync(Guid employeeId, CancellationToken cancellationToken);
    }
}
