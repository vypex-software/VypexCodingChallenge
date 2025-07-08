namespace Vypex.CodingChallenge.Application.Models;

public record ProjectDto
{
    public int Id { get; init; } = default!;

    public string Key { get; init; } = default!;

    public virtual ICollection<ProjectTaskDto> Tasks { get; init; } = [];
}
