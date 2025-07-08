using Vypex.CodingChallenge.Domain.Models;

namespace Vypex.CodingChallenge.Domain.Repositories;

public interface IProjectRepository
{
    Task<ICollection<Project>> QueryAsync(CancellationToken cancellationToken = default);

    Task<Project?> GetAsync(int id, CancellationToken cancellationToken = default);
}
