using FluentValidation;
using Vypex.CodingChallenge.Application.Commands.AddOrUpdateLeaveCommand;
using Vypex.CodingChallenge.Application.Models;

public class UpsertEmployeeLeaveCommandValidator : AbstractValidator<UpsertEmployeeLeaveCommand>
{
    public UpsertEmployeeLeaveCommandValidator()
    {
        RuleFor(x => x.employeeId)
            .NotEmpty().WithMessage("Employee ID is required.");

        RuleFor(x => x.leaves)
            .NotNull().WithMessage("Leave list is required.")
            .NotEmpty().WithMessage("At least one leave entry is required.");

        RuleForEach(x => x.leaves).ChildRules(leave =>
        {
            leave.RuleFor(l => l.StartDate)
                .NotEmpty().WithMessage("Start date is required.");

            leave.RuleFor(l => l.EndDate)
                .NotEmpty().WithMessage("End date is required.")
                .GreaterThan(l => l.StartDate).WithMessage("End date must be after start date.");
        });

        RuleFor(x => x.leaves)
            .Must(NotOverlap).WithMessage("Leave periods must not overlap.");
    }

    private bool NotOverlap(List<LeaveDTO> leaves)
    {
        if (leaves == null || leaves.Count < 2)
            return true;

        var sorted = leaves.OrderBy(l => l.StartDate).ToList();
        for (int i = 1; i < sorted.Count; i++)
        {
            if (sorted[i].StartDate < sorted[i - 1].EndDate)
                return false;
        }
        return true;
    }
}
