using Vypex.CodingChallenge.Application.Models;
using Vypex.CodingChallenge.Domain.Data;
using Vypex.CodingChallenge.Domain.Models;
using Vypex.CodingChallenge.Domain.Repositories;

namespace Vypex.CodingChallenge.Application.Services;

internal class ProjectService(IProjectRepository projectRepository, IUnitOfWork unitOfWork) : IProjectService
{
    public async Task<ICollection<ProjectSummaryDto>> GetProjectSummariesAsync(string? search, CancellationToken cancellationToken = default)
    {
        return (await projectRepository.QueryAsync(cancellationToken))
            .Where(project => search == null || project.Key.Contains(search))
            .Select(project => new ProjectSummaryDto
            {
                Id = project.Id,
                Key = project.Key,
                TotalPoints = project.Tasks.Sum(task => task.Points)
            })
            .ToList();
    }

    public async Task<ProjectDto?> GetProjectAsync(int id, CancellationToken cancellationToken = default)
    {
        var project = await projectRepository.GetAsync(id, cancellationToken);

        return project is null ? null : ToDto(project);
    }

    public async Task<ProjectDto?> UpdateProjectAsync(int id, ProjectUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var project = await projectRepository.GetAsync(id, cancellationToken);

        if (project is null)
        {
            return null;
        }

        project.Key = dto.Key;

        project.Tasks.Clear();

        foreach (var taskDto in dto.Tasks)
        {
            project.Tasks.Add(new ProjectTask
            {
                Id = taskDto.Id,
                ProjectId = project.Id,
                Description = taskDto.Description,
                Points = taskDto.Points
            });
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ToDto(project);
    }

    private static ProjectDto ToDto(Project project)
    {
        return new ProjectDto
        {
            Id = project.Id,
            Key = project.Key,
            Tasks = project.Tasks
                .Select(task => new ProjectTaskDto
                {
                    Id = task.Id,
                    Description = task.Description,
                    Points = task.Points
                })
                .ToList()
        };
    }
}
