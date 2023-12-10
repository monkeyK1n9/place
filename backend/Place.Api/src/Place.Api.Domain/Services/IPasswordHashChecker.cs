namespace Place.Api.Domain.Services;

/// <summary>
/// Represents a service for checking if a provided password matches a hashed password.
/// </summary>
public interface IPasswordHashChecker
{
    /// <summary>
    /// Checks if a provided password matches a hashed password.
    /// </summary>
    /// <param name="passwordHash">The hashed password to compare against.</param>
    /// <param name="providedPassword">The password provided for comparison.</param>
    /// <returns>True if the provided password matches the hashed password; otherwise, false.</returns>
    bool HashesMatch(string passwordHash, string providedPassword);
}
