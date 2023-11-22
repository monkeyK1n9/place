namespace Place.Api.Domain.Authentication.ValueObjects;

using ErrorOr;
using Place.Api.Domain.Common.Abstractions;

/// <summary>
/// Represents a first name.
/// </summary>
public class LastName : ValueObject
{
    /// <summary>
    /// The maximum length of a last name.
    /// </summary>
    private const int MaxLength = 256;

    /// <summary>
    /// The minimum length of a last name.
    /// </summary>
    private const int MinLength = 3;

    /// <summary>
    /// Gets the value of the last name.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="LastName"/> class.
    /// </summary>
    /// <param name="value">The value of the first name.</param>
    private LastName(string value)
        => this.Value = value;

    /// <summary>
    /// Creates a new instance of the <see cref="LastName"/> class.
    /// </summary>
    /// <param name="value">The value of the first name.</param>
    /// <returns>An instance of the <see cref="LastName"/> class or an error</returns>
    public static ErrorOr<LastName> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return DomainErrors.LastName.NullOrEmpty;
        }

        return value.Length switch
        {
            > MaxLength => DomainErrors.LastName.LongerThanAllowed,
            < MinLength => DomainErrors.LastName.LowerThanAllowed,
            _ => new LastName(value)
        };
    }


    /// <inheritdoc />
    public override string ToString() => this.Value;

    /// <inheritdoc/>
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.Value;
    }
}
