namespace Place.Api.Infrastructure.Swagger;

/// <summary>
/// Configuration settings for Swagger documentation in the application.
/// </summary>
public sealed class SwaggerOptions
{
    /// <summary>
    /// Gets or sets a value indicating whether Swagger is enabled.
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// Gets or sets the title of the Swagger documentation.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the version of the API documented by Swagger.
    /// </summary>
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the route where the Swagger UI will be available.
    /// </summary>
    public string Route { get; set; } = string.Empty;
}
