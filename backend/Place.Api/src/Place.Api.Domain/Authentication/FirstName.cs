namespace Place.Api.Domain.Authentication;

using Common.Abstractions;
using ErrorOr;

/// <summary>
/// Represents a first name.
/// </summary>
public class FirstName : ValueObject
{
    /// <summary>
    /// The maximum length of a first name.
    /// </summary>
    private const int MaxLength = 256;

    /// <summary>
    /// The minimum length of a first name.
    /// </summary>
    private const int MinLength = 3;

    /// <summary>
    /// Gets the value of the first name.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="FirstName"/> class.
    /// </summary>
    /// <param name="value">The value of the first name.</param>
    private FirstName(string value)
        => this.Value = value;

    /// <summary>
    /// Creates a new instance of the <see cref="FirstName"/> class.
    /// </summary>
    /// <param name="value">The value of the first name.</param>
    /// <returns>An instance of the <see cref="FirstName"/> class or an error</returns>
    public static ErrorOr<FirstName> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return DomainErrors.FirstName.NullOrEmpty;
        }

        return value.Length switch
        {
            > MaxLength => DomainErrors.FirstName.LongerThanAllowed,
            < MinLength => DomainErrors.FirstName.LowerThanAllowed,
            _ => new FirstName(value)
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
