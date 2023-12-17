namespace Place.Api.Presentation.Contracts.Authentication;

/// <summary>
/// Represents the request payload for user log in.
/// </summary>
/// <remarks>
/// Use this payload to be authenticated.
/// </remarks>
public record LoginRequest
{
    /// <summary>
    /// Email address of the user.
    /// </summary>
    /// <example>johndoe@gmail.com</example>
    public string Email { get; init; } = null!;

    /// <summary>
    /// Password for the user.
    /// </summary>
    /// <example>OnceUponATime123@</example>
    public string Password { get; init; } = null!;
}
