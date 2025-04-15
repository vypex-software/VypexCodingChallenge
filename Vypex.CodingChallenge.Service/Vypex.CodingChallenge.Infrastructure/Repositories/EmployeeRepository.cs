using Microsoft.EntityFrameworkCore;
using Vypex.CodingChallenge.Domain.Interfaces;
using Vypex.CodingChallenge.Domain.Models;
using Vypex.CodingChallenge.Infrastructure.Data;

namespace Vypex.CodingChallenge.Infrastructure.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext context, IUserContext userContext)
            : base(context, userContext)
        {
        }

        public async Task<List<EmployeeDTO>> GetAllWithTotalLeaveDaysAsync(CancellationToken cancellationToken)
        {
            var employees = await _context.Employees.Include(e => e.Leaves).ToListAsync(cancellationToken);

            return employees.Select(e => new EmployeeDTO
            {
                Id = e.Id,
                Name = e.Name,
                TotalLeaveDays = e.Leaves
                    .Where(l => l.DeletedOn == null)
                    .Sum(l => (l.EndDate.Date - l.StartDate.Date).Days + 1)
            }).ToList();
        }


        public Task<Employee?> GetEmployeeLeaveDetailsAsync(Guid employeeId, CancellationToken cancellationToken)
        {
            return _context.Employees
                    .Include(e => e.Leaves)
                    .Where(e => e.Id == employeeId)
                    .FirstOrDefaultAsync(cancellationToken);
        }

    }
}
