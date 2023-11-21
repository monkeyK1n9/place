namespace Place.Api.Domain.Common;

/// <summary>
/// Abstract class representing an entity.
/// </summary>
/// <typeparam name="TId">The type of the entity identifier.</typeparam>
public abstract class Entity<TId> : IEquatable<Entity<TId>>, IDomainEvent
    where TId : notnull
{
    private readonly List<IDomainEvent> domainEvents = new() { Capacity = 0 };

    /// <summary>
    /// Abstract class representing an entity.
    /// </summary>
    /// <typeparam name="TId">The type of the entity identifier.</typeparam>
    public TId Id { get; protected set; }

    /// <summary>
    /// Gets the domain events.
    /// </summary>
    /// <returns>The domain events.</returns>
    public IReadOnlyList<IDomainEvent> DomainEvents
        => this.domainEvents.AsReadOnly();

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{TId}"/> class.
    /// </summary>
    /// <param name="id">The entity identifier.</param>
    protected Entity(TId id) => this.Id = id;

    /// <inheritdoc/>
    public override bool Equals(object? obj)
        => obj is Entity<TId> entity && this.Id.Equals(entity.Id);


    /// <inheritdoc/>
    public bool Equals(Entity<TId>? other)
        => this.Equals((object?)other);

    /// <inheritdoc/>
    public static bool operator ==(Entity<TId> left, Entity<TId> right)
        => Equals(left, right);

    /// <inheritdoc/>
    public static bool operator !=(Entity<TId> left, Entity<TId> right)
        => !Equals(left, right);

    /// <inheritdoc/>
    public override int GetHashCode()
        => this.Id.GetHashCode();

    /// <summary>
    /// Adds a domain event.
    /// </summary>
    /// <param name="domainEvent">The domain event to add.</param>
    public void AddDomainEvent(IDomainEvent domainEvent)
        => this.domainEvents.Add(domainEvent);

    /// <summary>
    /// Clears the domain events.
    /// </summary>
    public void ClearDomainEvents()
        => this.domainEvents.Clear();


#pragma warning disable CS8618
    protected Entity() { }
#pragma warning restore CS8618
}
