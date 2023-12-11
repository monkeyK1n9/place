namespace Place.Api.Presentation;

using Errors;
using Mappings;
using Microsoft.AspNetCore.Mvc.Infrastructure;

public static class DependencyInjectionRegister
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services
            .AddSingleton<ProblemDetailsFactory, PlaceApiProblemDetailsFactory>()
            .AddMappings();
        return services;
    }
}
