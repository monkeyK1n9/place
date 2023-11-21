namespace Place.Api.Tests.Domain.Common;

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
        IDomainEvent? domainEvent = Substitute.For<IDomainEvent>();
        domainEvent.DomainEvents.Returns(new List<IDomainEvent>
        {
            Substitute.For<IDomainEvent>(), Substitute.For<IDomainEvent>()
        });

        // Act
        domainEvent.ClearDomainEvents();

        // Assert
        domainEvent.DomainEvents.Should().BeEmpty();
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
