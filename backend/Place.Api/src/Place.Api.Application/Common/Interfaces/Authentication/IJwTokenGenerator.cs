namespace Place.Api.Application.Common.Interfaces.Authentication
{
    using Domain.Authentication;

    /// <summary>
    /// Represents a service for generating JSON Web Tokens (JWT) for user authentication.
    /// </summary>
    public interface IJwTokenGenerator
    {
        /// <summary>
        /// Generates a JSON Web Token for the specified user.
        /// </summary>
        /// <param name="user">The user for whom the token is generated.</param>
        /// <returns>The generated JSON Web Token.</returns>
        string GenerateToken(User user);
    }
}
