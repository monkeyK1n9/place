namespace Place.Api.Domain.Authentication.ValueObjects;

using System;
using System.Collections.Generic;
using System.Linq;
using Common.Abstractions;
using ErrorOr;

/// <summary>
/// Represents a password.
/// </summary>
public sealed class Password : ValueObject
{
    private const int MinPasswordLength = 6;
    private static readonly Func<char, bool> IsLower = c => c >= 'a' && c <= 'z';
    private static readonly Func<char, bool> IsUpper = c => c >= 'A' && c <= 'Z';
    private static readonly Func<char, bool> IsDigit = c => c >= '0' && c <= '9';
    private static readonly Func<char, bool> IsNonAlphaNumeric = c => !(IsLower(c) || IsUpper(c) || IsDigit(c));

    /// <summary>
    /// Initializes a new instance of the <see cref="Password"/> class.
    /// </summary>
    /// <param name="value">The password value.</param>
    private Password(string value) => this.Value = value;

    /// <summary>
    /// Creates a new instance of the <see cref="Password"/> class.
    /// </summary>
    /// <param name="password">The password to validate and create.</param>
    /// <returns>An instance of the <see cref="Password"/> class or an error.</returns>
    public static ErrorOr<Password> Create(string password) =>
        password switch
        {
            null or "" => DomainErrors.Password.NullOrEmpty,
            { Length: < MinPasswordLength } => DomainErrors.Password.TooShort,
            { Length: >= MinPasswordLength } when !password.Any(IsLower) => DomainErrors.Password.MissingLowercaseLetter,
            { Length: >= MinPasswordLength } when !password.Any(IsUpper) => DomainErrors.Password.MissingUppercaseLetter,
            { Length: >= MinPasswordLength } when !password.Any(IsDigit) => DomainErrors.Password.MissingDigit,
            { Length: >= MinPasswordLength } when !password.Any(IsNonAlphaNumeric) => DomainErrors.Password.MissingNonAlphaNumeric,
            _ => new Password(password)
        };

    /// <summary>
    /// Implicitly converts a <see cref="Password"/> instance to a string.
    /// </summary>
    /// <param name="password">The password instance to convert.</param>
    public static implicit operator string(Password password) => password.Value;

    /// <summary>
    /// Gets the password value.
    /// </summary>
    public string Value { get; }

    /// <inheritdoc />
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.Value;
    }

    /// <inheritdoc />
    public override string ToString() => this.Value;
}
