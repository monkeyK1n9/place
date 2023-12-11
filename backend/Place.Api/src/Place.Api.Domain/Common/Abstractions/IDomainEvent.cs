namespace Place.Api.Domain.Common.Abstractions;

using System.Collections.Generic;

/// <summary>
/// Interface representing a domain event.
/// </summary>
public interface IDomainEvent
{
    /// <summary>
    /// Gets the domain events.
    /// </summary>
    /// <returns>The domain events.</returns>
    public IReadOnlyList<IDomainEvent> DomainEvents { get; }

    /// <summary>
    /// Clears the domain events.
    /// </summary>
    public void ClearDomainEvents();
}
