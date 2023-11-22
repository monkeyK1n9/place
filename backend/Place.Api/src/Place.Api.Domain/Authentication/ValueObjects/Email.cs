namespace Place.Api.Domain.Authentication.ValueObjects;

using System.Net.Mail;
using ErrorOr;
using Place.Api.Domain.Common.Abstractions;

/// <summary>
/// Represents an email address.
/// </summary>
public sealed class Email : ValueObject
{
    /// <summary>
    /// The email maximum length.
    /// </summary>
    public const int MaxLength = 256;

    /// <summary>
    /// Gets the email address.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Email"/> class.
    /// </summary>
    /// <param name="value">The email address.</param>
    private Email(string value)
        => this.Value = value;

    /// <summary>
    /// Creates a new instance of the <see cref="Email"/> class.
    /// </summary>
    /// <param name="value">The email address.</param>
    /// <returns>An instance of the <see cref="Email"/> class or an error</returns>
    public static ErrorOr<Email> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return DomainErrors.Email.NullOrEmpty;
        }

        if (!IsValidEmail(value))
        {
            return DomainErrors.Email.InvalidFormat;
        }

        if (value.Length > MaxLength)
        {
            return DomainErrors.Email.LongerThanAllowed;
        }

        return new Email(value);
    }

    /// <inheritdoc />
    public override string ToString() => this.Value;

    /// <inheritdoc/>
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.Value;
    }

    public static bool IsValidEmail(string email)
    {
        try
        {
            MailAddress addr = new(email);
            return addr.Address == email;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
