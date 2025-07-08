namespace Vypex.CodingChallenge.Application.Models;

public record ProjectTaskDto
{
    public int Id { get; init; }

    public string Description { get; init; } = default!;

    public int Points { get; init; }
}
