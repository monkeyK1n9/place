namespace Place.Api.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Place.Api.Infrastructure.Persistence.Authentication.EF.Contexts;
using Place.Api.Infrastructure.Persistence.Authentication.EF.Options;
using Place.Api.Infrastructure.Persistence.Interceptors;

public static class DependencyInjectionRegister
{
    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        PostgresOptions options = configuration.GetOptions<PostgresOptions>(PostgresOptions.SettingsKey);

        services.AddDbContext<UserReadDbContext>(context => context.UseNpgsql(options.ConnectionString));
        services.AddDbContext<UserWriteDbContext>(context => context.UseNpgsql(options.ConnectionString));
        services.AddScoped<SoftDeleteInterceptor>();
        services.AddScoped<UpdateAuditableEntitiesInterceptor>();
        return services;
    }
}
