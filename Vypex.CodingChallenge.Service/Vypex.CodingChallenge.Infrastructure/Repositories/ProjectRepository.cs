using Microsoft.EntityFrameworkCore;
using Vypex.CodingChallenge.Domain.Models;
using Vypex.CodingChallenge.Domain.Repositories;
using Vypex.CodingChallenge.Infrastructure.Data;

namespace Vypex.CodingChallenge.Infrastructure.Repositories;

internal class ProjectRepository(CodingChallengeContext dbContext) : IProjectRepository
{
    public async Task<ICollection<Project>> QueryAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Projects
            .Include(project => project.Tasks)
            .ToListAsync(cancellationToken);
    }

    public async Task<Project?> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Projects
            .Include(project => project.Tasks)
            .SingleOrDefaultAsync(project => project.Id == id, cancellationToken: cancellationToken);
    }
}
