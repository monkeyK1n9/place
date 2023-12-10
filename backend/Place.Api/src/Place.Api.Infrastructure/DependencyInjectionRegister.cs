namespace Place.Api.Infrastructure;

using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Services;
using Authentication;
using Cryptography;
using Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using Services;

public static class DependencyInjectionRegister
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuth(configuration);
        services.AddPostgres(configuration);
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddTransient<IPasswordHasher, PasswordHasher>();
        services.AddTransient<IPasswordHashChecker, PasswordHasher>();
        return services;
    }
}
