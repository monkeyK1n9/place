namespace Place.Api.Application;

using System.Reflection;
using Common.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionRegister
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddMediatR(typeof(DependencyInjectionRegister).Assembly);

        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
