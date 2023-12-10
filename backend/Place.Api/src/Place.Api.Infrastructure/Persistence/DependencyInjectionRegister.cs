namespace Place.Api.Infrastructure.Persistence;

using Application.Common.Interfaces.Authentication;
using EF.Authentication.Repositories;
using EF.Contexts;
using EF.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Place.Api.Infrastructure.Persistence.Interceptors;

public static class DependencyInjectionRegister
{
    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        PostgresOptions options = configuration.GetOptions<PostgresOptions>(PostgresOptions.SettingsKey);

        services.AddDbContext<ReadDbContext>(context => context.UseNpgsql(options.ConnectionString));
        services.AddDbContext<WriteDbContext>(context => context.UseNpgsql(options.ConnectionString));

        services.AddScoped<SoftDeleteInterceptor>();
        services.AddScoped<UpdateAuditableEntitiesInterceptor>();

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
