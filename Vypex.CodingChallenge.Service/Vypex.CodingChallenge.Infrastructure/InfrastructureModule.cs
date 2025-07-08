using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Vypex.CodingChallenge.Domain.Data;
using Vypex.CodingChallenge.Domain.Repositories;
using Vypex.CodingChallenge.Infrastructure.Data;
using Vypex.CodingChallenge.Infrastructure.Repositories;

namespace Vypex.CodingChallenge.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructureModule(this IServiceCollection services, string connectionString)
    {
        return services
            .AddDbContext<CodingChallengeContext>(options => options.UseSqlite(connectionString))
            .AddTransient<IProjectRepository, ProjectRepository>()
            .AddTransient<IUnitOfWork>(sp => sp.GetRequiredService<CodingChallengeContext>());
    }
}
