namespace Place.Api.Presentation.Contrats.Authentication;

/// <summary>
/// Represents the request payload to send recovery otp.
/// </summary>
/// <remarks>
/// Use this payload generates an otp sent by mail to the user for account recovery.
/// </remarks>
public record SendOTPRequest
{
    /// <summary>
    /// Email address of the user.
    /// </summary>
    /// <example>johndoe@gmail.com</example>
    public string Email { get; init; } = null!;

}

/// <summary>
/// Represents the request payload to change user password.
/// </summary>
/// <remarks>
/// Use this payload change the password of the user.
/// </remarks>
public record OTPVerificationRequest
{
    /// <summary>
    /// New password for the user.
    /// </summary>
    /// <example>OnceUponATime123@</example>
    public string Password { get; init; } = null!;

    /// <summary>
    /// Confirmation password to recover account. It should match the main <see cref="Password"/> value.
    /// </summary>
    /// <example>OnceUponATime123@</example>
    public string ConfirmPassword { get; init; } = null!;
}
