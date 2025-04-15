using Vypex.CodingChallenge.Domain.Interfaces;
using Vypex.CodingChallenge.Domain.Models;
using Vypex.CodingChallenge.Infrastructure.Data;

namespace Vypex.CodingChallenge.Infrastructure.Repositories;

public class LeaveRepository : GenericRepository<Leave>, ILeaveRepository
{
    public LeaveRepository(ApplicationDbContext context, IUserContext userContext)
            : base(context, userContext)
    {
    }
}
