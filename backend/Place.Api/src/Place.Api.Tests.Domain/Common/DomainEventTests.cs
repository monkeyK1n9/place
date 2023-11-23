namespace Place.Api.Tests.Domain.Common;

using System;
using System.Collections.Generic;
using Api.Domain.Common.Abstractions;
using FluentAssertions;
using NSubstitute;

public class DomainEventTests
{
    [Fact]
    public void DomainEventsShouldBeEmptyByDefault()
    {
        // Arrange
        IDomainEvent? domainEvent = Substitute.For<IDomainEvent>();

        // Act
        IReadOnlyList<IDomainEvent> result = domainEvent.DomainEvents;

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void ClearDomainEventsShouldRemoveAllDomainEvents()
    {
        // Arrange
        Entity<Guid>? entity1 = Substitute.For<Entity<Guid>>();
        IDomainEvent? domainEvent = Substitute.For<IDomainEvent>();

        entity1.AddDomainEvent(domainEvent);
        entity1.AddDomainEvent(domainEvent);
        // Act
        entity1.ClearDomainEvents();

        // Assert
        entity1.DomainEvents.Should().BeEmpty();
    }

    [Fact]
    public void DomainEventsShouldContainAllAddedDomainEvents()
    {
        Entity<Guid>? entity1 = Substitute.For<Entity<Guid>>();
        // Arrange
        IDomainEvent? domainEvent1 = Substitute.For<IDomainEvent>();
        IDomainEvent? domainEvent2 = Substitute.For<IDomainEvent>();

        // Act
        entity1.AddDomainEvent(domainEvent1);
        entity1.AddDomainEvent(domainEvent2);

        // Assert
        entity1.DomainEvents.Should().BeEquivalentTo(new List<IDomainEvent> { domainEvent1, domainEvent2 });
    }
}
