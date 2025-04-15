using Microsoft.Extensions.DependencyInjection;
using Vypex.CodingChallenge.Domain;
using Vypex.CodingChallenge.Domain.Models;

namespace Vypex.CodingChallenge.Infrastructure.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        try
        {
            db.Database.EnsureCreated(); // Or db.Database.Migrate()

            if (!db.Employees.Any())
            {
                var employees = FakeEmployeesSeed.Generate(20);
                db.Employees.AddRange(employees);
                db.Leaves.AddRange(employees.First().Leaves);
                db.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Seeding failed: {ex.Message}");
        }

    }
}
