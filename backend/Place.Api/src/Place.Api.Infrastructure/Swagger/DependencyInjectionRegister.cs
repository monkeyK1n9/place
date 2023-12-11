namespace Place.Api.Infrastructure.Swagger;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

public static class DependencyInjectionRegister
{
    /// <summary>
    /// Adds Swagger documentation services to the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <param name="configuration">The configuration instance.</param>
    /// <returns>The modified service collection.</returns>
    public static IServiceCollection AddSwaggerDocs(this IServiceCollection services, IConfiguration configuration)
    {
        IConfigurationSection section = configuration.GetSection("swagger");

        SwaggerOptions options = section.BindOptions<SwaggerOptions>();

        services.Configure<SwaggerOptions>(section);

        if (!options.Enabled)
        {
            return services;
        }

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(swagger =>
        {
            swagger.SchemaFilter<ExcludePropertiesFilter>();
            swagger.EnableAnnotations();
            swagger.CustomSchemaIds(x => x.FullName);
            swagger.SwaggerDoc(options.Version, new OpenApiInfo { Title = options.Title, Version = options.Version });
            swagger.SupportNonNullableReferenceTypes();



            List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
            foreach (string xmlFilePath in xmlFiles
                         .Select(fileName => Path.Combine(AppContext.BaseDirectory, fileName))
                         .Where(File.Exists))
            {
                swagger.IncludeXmlComments(xmlFilePath, includeControllerXmlComments: true);
            }
        });

        return services;
    }

    /// <summary>
    /// Configures Swagger middleware in the application.
    /// </summary>
    /// <param name="app">The application builder.</param>
    /// <returns>The modified application builder.</returns>
    public static IApplicationBuilder UseSwaggerDocs(this IApplicationBuilder app)
    {
        SwaggerOptions options = app.ApplicationServices.GetRequiredService<IOptions<SwaggerOptions>>().Value;

        if (!options.Enabled)
        {
            return app;
        }

        app.UseSwagger();

        app.UseSwaggerUI(config =>
        {
            config.RoutePrefix = string.IsNullOrWhiteSpace(options.Route) ? "swagger" : options.Route;

            config.DocumentTitle = options.Title;
        });

        return app;
    }
}
