namespace Place.Api.Infrastructure.Persistence.Authentication.EF;

using Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Options;

public static class DependencyInjectionRegister
{
    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        PostgresOptions options = configuration.GetOptions<PostgresOptions>(PostgresOptions.SettingsKey);

        services.AddDbContext<UserReadDbContext>( context => context.UseNpgsql(options.ConnectionString));

        return services;
    }
}
