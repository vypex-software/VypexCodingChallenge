using Microsoft.EntityFrameworkCore;
using Vypex.CodingChallenge.Domain.Interfaces;
using Vypex.CodingChallenge.Domain.Models;
using Vypex.CodingChallenge.Infrastructure.Persistence.Configurations;

namespace Vypex.CodingChallenge.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public DbSet<Employee> Employees { get; set; } = default!;
        public DbSet<Leave> Leaves { get; set; } = default!;

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new LeaveConfiguration());
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException dbuEx)
            {
                throw new Exception("A database update error occurred.", dbuEx);
            }
        }
    }
}
