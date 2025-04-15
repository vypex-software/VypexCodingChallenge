namespace Vypex.CodingChallenge.Domain.Models;

public class Employee
{
    public Guid Id { get; set; }
    public required string Name { get; set; }

    public ICollection<Leave> Leaves { get; set; } = new List<Leave>();
}
