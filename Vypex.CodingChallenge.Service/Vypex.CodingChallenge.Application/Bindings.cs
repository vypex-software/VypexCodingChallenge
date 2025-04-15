using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Vypex.CodingChallenge.Application.Interfaces;
using Vypex.CodingChallenge.Application.PipelineBehavior;
using Vypex.CodingChallenge.Application.Services;

namespace Vypex.CodingChallenge.Application;

public static class Bindings
{
    public static IServiceCollection AddApplication(
        this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Bindings).Assembly));
        serviceCollection.AddValidatorsFromAssembly(typeof(Bindings).Assembly);

        serviceCollection.AddTransient<IEmployeeService, EmployeeService>();
        serviceCollection.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        serviceCollection.AddTransient<ILeaveService, LeaveService>();
        return serviceCollection;
    }
}