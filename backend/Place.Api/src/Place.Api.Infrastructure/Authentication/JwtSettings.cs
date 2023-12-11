namespace Place.Api.Infrastructure.Authentication;

/// <summary>
/// Represents the settings necessary for configuring JSON Web Token (JWT) authentication.
/// </summary>
public class JwtSettings
{
    /// <summary>
    /// Gets the section name in the configuration file for JWT settings.
    /// </summary>
    public static string SectionName { get; } = "JwtSettings";

    /// <summary>
    /// Gets the secret key used for signing the JWT.
    /// </summary>
    /// <remarks>
    /// The secret key should be a random string that is kept secret.
    /// It is used to sign and verify JWT tokens to ensure their integrity and authenticity.
    /// </remarks>
    public string Secret { get; init; } = null!;

    /// <summary>
    /// Gets the issuer of the JWT.
    /// </summary>
    /// <remarks>
    /// The issuer is typically the web server that issues the JWT.
    /// It is used to prevent tokens issued by one server from being accepted by another.
    /// </remarks>
    public string Issuer { get; init; } = null!;

    /// <summary>
    /// Gets the intended audience of the JWT.
    /// </summary>
    /// <remarks>
    /// The audience is the intended recipient of the JWT.
    /// It is used to ensure that the JWT is sent to the correct recipient.
    /// </remarks>
    public string Audience { get; init; } = null!;

    /// <summary>
    /// Gets the expiry time in minutes for the JWT.
    /// </summary>
    /// <remarks>
    /// This value determines how long the JWT will remain valid after it is issued.
    /// After this time has passed, the JWT will need to be refreshed or a new one will need to be issued.
    /// </remarks>
    public int ExpiryInMinutes { get; init; }
}
