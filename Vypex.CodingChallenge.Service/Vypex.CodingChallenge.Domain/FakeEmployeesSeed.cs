using Bogus;
using Vypex.CodingChallenge.Domain.Models;

namespace Vypex.CodingChallenge.Domain;

public static class FakeEmployeesSeed
{
public static List<Employee> Generate(int count)
{
    var faker = new Faker();

    return Enumerable.Range(1, count).Select(_ =>
    {
        var id = Guid.NewGuid();

        return new Employee
        {
            Id = id,
            Name = faker.Name.FullName(),
            Leaves = new List<Leave>
            {
                new Leave
                {
                    LeaveId = Guid.NewGuid(),
                    StartDate = DateTime.Now.AddDays(-faker.Random.Int(0, 10)),
                    EndDate = DateTime.Now.AddDays(faker.Random.Int(1, 10)),
                    EmployeeId = id
                }
            }
        };
    }).ToList();
}}
