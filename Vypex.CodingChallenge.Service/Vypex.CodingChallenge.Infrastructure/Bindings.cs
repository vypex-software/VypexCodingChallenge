using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Vypex.CodingChallenge.Domain.Interfaces;
using Vypex.CodingChallenge.Infrastructure.Data;
using Vypex.CodingChallenge.Infrastructure.Repositories;

namespace Vypex.CodingChallenge.Infrastructure;

public static class Bindings
{
    public static IServiceCollection AddInfrastructureModule(this IServiceCollection serviceCollection,
        string connectionString)
    {
        serviceCollection.AddDbContext<ApplicationDbContext>(options => options
            .UseSqlite(connectionString));
        serviceCollection.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        serviceCollection.AddScoped<IEmployeeRepository, EmployeeRepository>();
        serviceCollection.AddTransient<ILeaveRepository, LeaveRepository>();
        serviceCollection.AddScoped<IUserContext, HttpUserContext>();
        serviceCollection.AddScoped<IUnitOfWork>(serviceProvider =>
        serviceProvider.GetRequiredService<ApplicationDbContext>());

        return serviceCollection;
    }
}
