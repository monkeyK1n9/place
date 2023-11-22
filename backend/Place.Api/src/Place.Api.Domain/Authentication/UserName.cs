namespace Place.Api.Domain.Authentication;

using Common.Abstractions;
using ErrorOr;

/// <summary>
/// Represents a user name.
/// </summary>
public class UserName : ValueObject
{
    /// <summary>
    /// The maximum length of a user name.
    /// </summary>
    private const int MaxLength = 12;

    /// <summary>
    /// The minimum length of a user name.
    /// </summary>
    private const int MinLength = 5;

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
            _ => new UserName(value)
        };
    }

    /// <inheritdoc />
    public override string ToString() => this.Value;

    /// <inheritdoc />
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.Value;
    }
}
