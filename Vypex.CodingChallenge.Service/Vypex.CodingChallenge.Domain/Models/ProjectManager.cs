namespace Vypex.CodingChallenge.Domain.Models;

public class ProjectManager
{
    public int Id { get; set; }

    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public int ProjectId { get; set; }

    public virtual Project Project { get; set; } = default!;
}
