using Microsoft.Extensions.DependencyInjection;
using Vypex.CodingChallenge.Application.Services;

namespace Vypex.CodingChallenge.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplicationModule(this IServiceCollection services)
    {
        return services
            .AddTransient<IProjectService, ProjectService>();
    }
}
