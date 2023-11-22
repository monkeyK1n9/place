namespace Place.Api.Domain.Authentication;

using Common.Abstractions;
using ErrorOr;

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
}
