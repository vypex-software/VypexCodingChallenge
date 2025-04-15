using Microsoft.Extensions.DependencyInjection;

namespace Vypex.CodingChallenge.Domain
{
    public static class Bindings
    {
        public static IServiceCollection AddDomainModule(this IServiceCollection services)
        {
            return services;
        }
    }
}
