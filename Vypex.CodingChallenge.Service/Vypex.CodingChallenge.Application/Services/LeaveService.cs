using Vypex.CodingChallenge.Application.Interfaces;
using Vypex.CodingChallenge.Application.Models;
using Vypex.CodingChallenge.Domain.Interfaces;
using Vypex.CodingChallenge.Domain.Models;

namespace Vypex.CodingChallenge.Application.Services;

public class LeaveService : ILeaveService
{
    private readonly IEmployeeRepository _repo;
    private readonly ILeaveRepository _leaveRepo;
    public LeaveService(IEmployeeRepository empRepository, ILeaveRepository leaveRepo)
    {
        _repo = empRepository;
        _leaveRepo = leaveRepo;
    }
    public async Task<bool> UpsertLeavesAsync(Guid employeeId, IEnumerable<LeaveDTO> leaves, CancellationToken cancellationToken)
    {
        if (leaves is null || !leaves.Any())
            return false;

        // Fetch existing employee's leaves
        var existingEmployee = await _repo.GetEmployeeLeaveDetailsAsync(employeeId, cancellationToken);
        if (existingEmployee is null)
        {
            return false;
        }

        // Identify leaves to update, add, or delete
        var existingLeaves = existingEmployee.Leaves.ToList();
        var leavesToDelete = new List<Leave>();
        var leavesToAdd = new List<Leave>();

        foreach (var newLeave in leaves)
        {
            if (newLeave.LeaveId == Guid.Empty)
            {
                var leaveEntity = MapToEntity(employeeId, newLeave);
                await _leaveRepo.AddAsync(leaveEntity, cancellationToken);
                continue;
            }
            var existingLeave = existingLeaves
                .FirstOrDefault(l => l.LeaveId == newLeave.LeaveId);

            if (existingLeave is not null)
            {
                // Update leave if there are changes
                if (existingLeave.StartDate != newLeave.StartDate || existingLeave.EndDate != newLeave.EndDate)
                {
                    existingLeave.StartDate = newLeave.StartDate;
                    existingLeave.EndDate = newLeave.EndDate;
                    _leaveRepo.Update(existingLeave, cancellationToken);
                }
                existingLeaves.Remove(existingLeave);
            }
        }

        // Delete leaves that no longer exist in the new list
        foreach (var leaveToDelete in existingLeaves)
        {
            _leaveRepo.Delete(leaveToDelete, cancellationToken);
            leavesToDelete.Add(leaveToDelete);
        }

        return true;
    }

    private Leave MapToEntity(Guid employeeId, LeaveDTO dto)
    {
        return new Leave
        {
            LeaveId = Guid.NewGuid(),
            EmployeeId = employeeId,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate
        };
    }

}

