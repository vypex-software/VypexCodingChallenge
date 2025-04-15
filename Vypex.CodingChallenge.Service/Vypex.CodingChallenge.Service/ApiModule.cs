namespace Vypex.CodingChallenge.Service;

public static class ApiModule
{
    public static IServiceCollection AddApiModule(this IServiceCollection services)
    {
        return services;
    }

    public static IMvcBuilder AddApiControllers(this IMvcBuilder builder)
    {
        return builder.AddApplicationPart(typeof(ApiModule).Assembly);
    }
}
