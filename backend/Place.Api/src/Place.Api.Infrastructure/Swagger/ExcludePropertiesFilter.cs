namespace Place.Api.Infrastructure.Swagger;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Humanizer;
using Microsoft.OpenApi.Models;
using Place.Api.Infrastructure.Swagger.Attributes;
using Swashbuckle.AspNetCore.SwaggerGen;

/// <summary>
/// Schema filter to exclude properties marked with the <see cref="HiddenAttribute"/>
/// from the Swagger documentation.
/// </summary>
internal sealed class ExcludePropertiesFilter : ISchemaFilter
{
    private const BindingFlags Flags = BindingFlags.Public | BindingFlags.Instance;
    private static readonly ConcurrentDictionary<Type, IDictionary<string, OpenApiSchema>> Properties = new();

    /// <summary>
    /// Applies the schema filter to exclude specified properties from Swagger documentation.
    /// </summary>
    /// <param name="schema">The OpenAPI schema being modified.</param>
    /// <param name="context">The context information for the schema filter.</param>
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema.Properties.Count == 0)
        {
            return;
        }

        if (Properties.TryGetValue(context.Type, out IDictionary<string, OpenApiSchema>? properties))
        {
            schema.Properties = properties;
            return;
        }

        IEnumerable<string> excludedProperties = context.Type
            .GetProperties(Flags)
            .Where(x => x.GetCustomAttribute<HiddenAttribute>() is not null)
            .Select(x => x.Name.Camelize());

        foreach (string excludedProperty in excludedProperties)
        {
            if (schema.Properties.ContainsKey(excludedProperty))
            {
                schema.Properties.Remove(excludedProperty);
            }
        }

        Properties.TryAdd(context.Type, schema.Properties);
    }
}
