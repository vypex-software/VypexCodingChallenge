using Vypex.CodingChallenge.Application.Models;

namespace Vypex.CodingChallenge.Application.Services;

public interface IProjectService
{
    Task<ICollection<ProjectSummaryDto>> GetProjectSummariesAsync(string? search, CancellationToken cancellationToken = default);

    Task<ProjectDto?> GetProjectAsync(int id, CancellationToken cancellationToken = default);

    Task<ProjectDto?> UpdateProjectAsync(int id, ProjectUpdateDto project, CancellationToken cancellationToken = default);
}
