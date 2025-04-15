using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vypex.CodingChallenge.Domain.Models;

namespace Vypex.CodingChallenge.Infrastructure.Persistence.Configurations;

public class LeaveConfiguration : IEntityTypeConfiguration<Leave>
{
    public void Configure(EntityTypeBuilder<Leave> builder)
    {
        builder.HasKey(l => l.LeaveId);

        builder.Property(l => l.StartDate).IsRequired();
        builder.Property(l => l.EndDate).IsRequired();
        builder.Property(l => l.EmployeeId).IsRequired();

        builder.Property(l => l.Createdon).IsRequired();
        builder.Property(l => l.CreatedBy).IsRequired().HasMaxLength(100);

        builder.Property(l => l.ModifiedOn);
        builder.Property(l => l.ModifiedBy).HasMaxLength(100);

        builder.Property(l => l.DeletedOn);
        builder.Property(l => l.DeletedBy).HasMaxLength(100);

        builder.HasOne<Employee>()
            .WithMany()
            .HasForeignKey(l => l.EmployeeId);
    }
}
