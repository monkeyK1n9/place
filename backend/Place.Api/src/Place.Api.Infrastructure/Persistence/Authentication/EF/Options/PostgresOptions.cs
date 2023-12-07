namespace Place.Api.Infrastructure.Persistence.Authentication.EF.Options;

using System;

/// <summary>
/// Represents the settings required to configure the PostgreSQL database connection.
/// </summary>
internal sealed class PostgresOptions
{
    /// <summary>
    /// Gets the key used to retrieve the PostgreSQL settings from the configuration file.
    /// </summary>
    public static string SettingsKey => "Postgres";

    private string connectionString = string.Empty;

    /// <summary>
    /// Gets or sets the connection string for the PostgreSQL database.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when the connection string is null or empty.</exception>
    public string ConnectionString
    {
        get => this.connectionString;
        set => this.connectionString = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new ArgumentException("Connection string must not be null or empty.",
                nameof(this.ConnectionString));
    }
}
