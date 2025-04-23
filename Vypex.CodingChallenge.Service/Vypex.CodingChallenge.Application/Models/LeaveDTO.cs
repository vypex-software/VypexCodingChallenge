namespace Vypex.CodingChallenge.Application.Models;

public class LeaveDTO
{
    public Guid LeaveId { get; set; }
    public required DateTime StartDate { get; init; }
    public required DateTime EndDate { get; init; }
}

