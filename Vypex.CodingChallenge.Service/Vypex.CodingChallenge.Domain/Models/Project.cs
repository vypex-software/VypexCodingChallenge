namespace Vypex.CodingChallenge.Domain.Models;

public class Project
{
    public int Id { get; set; } = default!;

    public string Key { get; set; } = default!;

    public virtual ICollection<ProjectTask> Tasks { get; set; } = [];

    public virtual ProjectManager Manager { get; set; } = default!;
}
