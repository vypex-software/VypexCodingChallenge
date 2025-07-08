namespace Vypex.CodingChallenge.Domain.Models;

public class ProjectTask
{
    public int Id { get; set; }

    public int ProjectId { get; set; } = default!;

    public virtual Project Project { get; set; } = default!;

    public string Description { get; set; } = default!;

    public int Points { get; set; }
}