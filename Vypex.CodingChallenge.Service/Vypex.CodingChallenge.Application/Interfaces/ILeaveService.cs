using Vypex.CodingChallenge.Application.Models;

namespace Vypex.CodingChallenge.Application.Interfaces;

public interface ILeaveService {
    Task<bool> UpsertLeavesAsync(Guid employeeId, IEnumerable<LeaveDTO> model, CancellationToken cancellationToken);
}
