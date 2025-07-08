namespace Vypex.CodingChallenge.Application.Models;

public record ProjectUpdateDto
{
    public string Key { get; init; } = default!;

    public ICollection<ProjectTaskDto> Tasks { get; init; } = [];
}
