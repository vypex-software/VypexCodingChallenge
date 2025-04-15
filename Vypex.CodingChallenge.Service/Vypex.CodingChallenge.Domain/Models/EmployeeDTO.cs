namespace Vypex.CodingChallenge.Domain.Models;

public class EmployeeDTO
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public int TotalLeaveDays { get; set; }
}
