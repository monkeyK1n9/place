namespace Place.Api.Domain.Authentication;

using Common.Abstractions;

/// <summary>
/// Represents a user ID.
/// </summary>
public sealed class UserId : AggregateRootId<Ulid>
{
    /// <summary>
    /// Gets or sets the value of the user ID.
    /// </summary>
    public override Ulid Value { get; protected set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UserId"/> class.
    /// </summary>
    /// <param name="value">The value of the user ID.</param>
    private UserId(Ulid value)
        => this.Value = value;

    /// <summary>
    /// Creates a new instance of the <see cref="UserId"/> class with a unique value.
    /// </summary>
    /// <returns>An instance of the <see cref="UserId"/> class.</returns>
    public static UserId CreateUnique()
        => new(Ulid.NewUlid());

    /// <summary>
    /// Creates a new instance of the <see cref="UserId"/> class with the specified value.
    /// </summary>
    /// <param name="value">The value of the user ID.</param>
    /// <returns>An instance of the <see cref="UserId"/> class or an error</returns>
    public static UserId Create(Ulid value)
        => new(value);

    /// <inheritdoc/>
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.Value;
    }

    private UserId() { }
}
