using Vypex.CodingChallenge.Application.Models;

namespace Vypex.CodingChallenge.Application.Commands.AddOrUpdateLeaveCommand;

public record UpsertEmployeeLeaveCommand(Guid employeeId, List<LeaveDTO> leaves) : ICommand<bool>;