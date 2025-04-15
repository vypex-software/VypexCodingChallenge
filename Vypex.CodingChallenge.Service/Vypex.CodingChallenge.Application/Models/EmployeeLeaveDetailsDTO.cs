namespace Vypex.CodingChallenge.Application.Models
{
    public class EmployeeLeaveDetailsDTO
    {
        public Guid EmployeeId { get; set; }
        public IEnumerable<LeaveDTO> Leaves { get; set; } = new List<LeaveDTO>();
    }
}
