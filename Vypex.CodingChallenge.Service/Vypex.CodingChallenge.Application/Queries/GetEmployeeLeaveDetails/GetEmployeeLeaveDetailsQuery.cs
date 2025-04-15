using Vypex.CodingChallenge.Application.Models;

namespace Vypex.CodingChallenge.Application.Queries.GetEmployeeLeaveDetails;

public class GetEmployeeLeaveDetailsQuery : IQuery<EmployeeLeaveDetailsDTO>
{
    public Guid EmployeeId { get; set; }

}
