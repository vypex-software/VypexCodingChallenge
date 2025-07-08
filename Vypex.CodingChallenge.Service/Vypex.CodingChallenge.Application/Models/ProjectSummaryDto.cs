namespace Vypex.CodingChallenge.Application.Models;

public record ProjectSummaryDto
{
    public int Id { get; init; } = default!;

    public string Key { get; init; } = default!;

    public int TotalPoints { get; init; }
}
