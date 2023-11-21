namespace Place.Api.Domain.Common;

/// <summary>
/// Abstract class representing an aggregate root.
/// </summary>
/// <typeparam name="TId">The type of the aggregate root identifier.</typeparam>
/// <typeparam name="TIdType">The type of the aggregate root identifier value.</typeparam>
public abstract class AggregateRoot<TId, TIdType> : Entity<TId>
    where TId : AggregateRootId<TIdType>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AggregateRoot{TId, TIdType}"/> class.
    /// </summary>
    /// <param name="id">The aggregate root identifier.</param>
    protected AggregateRoot(TId id)
        => this.Id = id;

    /// <summary>
    /// Initializes a new instance of the <see cref="AggregateRoot{TId, TIdType}"/> class.
    /// </summary>
    protected AggregateRoot()
    {
    }
}
