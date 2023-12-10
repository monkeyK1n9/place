namespace Place.Api.Presentation.Mappings;

using System.Reflection;
using Mapster;

public static class DependencyInjectionRegister
{
    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        TypeAdapterConfig config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddMapster();
        return services;
    }
}
