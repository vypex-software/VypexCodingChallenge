using Microsoft.EntityFrameworkCore;
using Vypex.CodingChallenge.Domain.Data;
using Vypex.CodingChallenge.Domain.Models;

namespace Vypex.CodingChallenge.Infrastructure.Data;

public class CodingChallengeContext(DbContextOptions<CodingChallengeContext> options) : DbContext(options), IUnitOfWork
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>()
            .HasIndex(p => p.Key)
            .IsUnique();
    }

    public DbSet<Project> Projects { get; set; } = default!;
}
