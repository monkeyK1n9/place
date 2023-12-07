namespace Place.Api.Infrastructure;

using Application.Common.Interfaces.Services;
using Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using Services;

internal static class DependencyInjectionRegister
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuth(configuration);
        services.AddPostgres(configuration);
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        return services;
    }
}
