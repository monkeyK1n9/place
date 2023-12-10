namespace Place.Api.Domain.Authentication.ValueObjects;

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using ErrorOr;
using Place.Api.Domain.Common.Abstractions;

/// <summary>
/// Represents a user name.
/// </summary>
public sealed class UserName : ValueObject
{
    /// <summary>
    /// The maximum length of a user name.
    /// </summary>
    public const int MaxLength = 12;

    /// <summary>
    /// The minimum length of a user name.
    /// </summary>
    public const int MinLength = 5;

    /// <summary>
    /// Gets the value of the user name.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UserName"/> class.
    /// </summary>
    /// <param name="value">The value of the user name.</param>
    private UserName(string value)
        => this.Value = value;

    /// <summary>
    /// Creates a new instance of the <see cref="UserName"/> class.
    /// </summary>
    /// <param name="value">The value of the user name.</param>
    /// <returns>An instance of the <see cref="UserName"/> class or an error</returns>
    public static ErrorOr<UserName> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return DomainErrors.UserName.NullOrEmpty;
        }

        return value.Length switch
        {
            > MaxLength => DomainErrors.UserName.LongerThanAllowed,
            < MinLength => DomainErrors.UserName.LowerThanAllowed,
            _ => new UserName(GenerateRandomUsername(value))
        };
    }

    /// <summary>
    /// Creates a new instance of the <see cref="UserName"/> class based on the provided email.
    /// </summary>
    /// <param name="email">The email address used to generate the username.</param>
    /// <returns>An instance of the <see cref="UserName"/> class based on the email.</returns>
    public static ErrorOr<UserName> Create(Email email) =>
        new UserName(GenerateRandomUsername(email.Value));

    /// <inheritdoc />
    public override string ToString() => this.Value;

    /// <inheritdoc />
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.Value;
    }

    /// <summary>
    /// Generates a random username based on the given email address.
    /// </summary>
    /// <param name="email">The email address used to generate the username.</param>
    /// <returns>A random username based on the email address.</returns>
    private static string GenerateRandomUsername(string email)
    {
        string username = email.Split('@')[0];

        if (username.Length > MaxLength)
        {
            username = username.Substring(0, 8);
        }

        int remainingSpace = Math.Max(0, MaxLength - username.Length);

        string randomSuffix = GenerateRandomSuffix(remainingSpace);

        StringBuilder builder = new();
        builder.Append(username)
            .Append(randomSuffix);

        string randomUsername = builder.ToString();

        return randomUsername;
    }

    /// <summary>
    /// Generates a cryptographically secure random suffix with a specified maximum length.
    /// </summary>
    /// <param name="maxLength">The maximum length of the random suffix.</param>
    /// <returns>A random suffix with a length not exceeding the specified maximum length.</returns>
    private static string GenerateRandomSuffix(int maxLength)
    {
        // Generate a cryptographically secure random suffix with a specified maximum length
        byte[] randomBytes = new byte[maxLength / 2];
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }

        // Convert the random bytes to a hexadecimal string
        string randomSuffix = BitConverter.ToString(randomBytes)
            .Replace("-", "", StringComparison.Ordinal)
            .ToUpperInvariant();

        return randomSuffix;
    }
}
