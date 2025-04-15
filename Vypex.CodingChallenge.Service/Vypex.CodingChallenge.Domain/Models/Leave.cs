namespace Vypex.CodingChallenge.Domain.Models;

public class Leave: AuditableEntity
{
    public Guid LeaveId { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public required Guid EmployeeId { get; init; }
}
