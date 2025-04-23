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

        public async Task<List<Employee>> GetAllWithTotalLeaveDaysAsync(CancellationToken cancellationToken)
        {
            return await _context.Employees.Include(e => e.Leaves).ToListAsync(cancellationToken);
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
