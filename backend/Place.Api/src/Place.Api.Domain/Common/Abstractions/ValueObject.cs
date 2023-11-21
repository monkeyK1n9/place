namespace Place.Api.Domain.Common.Abstractions;

/// <summary>
/// Abstract class representing a value object.
/// </summary>
public abstract class ValueObject : IEquatable<ValueObject>
{
    /// <summary>
    /// Gets the equality components of the value object.
    /// </summary>
    /// <returns>The equality components of the value object.</returns>
    public abstract IEnumerable<object> GetEqualityComponents();

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != this.GetType())
        {
            return false;
        }

        ValueObject valueObject = (ValueObject)obj;
        return this.GetEqualityComponents()
            .SequenceEqual(valueObject.GetEqualityComponents());
    }
    public static bool operator ==(ValueObject left, ValueObject right)
        => Equals(left, right);

    public static bool operator !=(ValueObject left, ValueObject right)
        => !Equals(left, right);

    /// <inheritdoc/>
    public override int GetHashCode()
        => this.GetEqualityComponents()
            .Select(x => x?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);

    /// <inheritdoc/>
    public bool Equals(ValueObject? other)
    => this.Equals((object?)other);
}
