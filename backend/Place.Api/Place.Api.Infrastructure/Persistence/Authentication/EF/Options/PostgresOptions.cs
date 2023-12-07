namespace Place.Api.Infrastructure.Persistence.Authentication.EF.Options;

public class PostgresOptions
{
    public static string SettingsKey => "Postgres";
    public string ConnectionString { get; set; }
}
