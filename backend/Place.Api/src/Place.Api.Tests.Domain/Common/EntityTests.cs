namespace Place.Api.Tests.Domain.Common;

using System;
using Api.Domain.Common.Abstractions;
using FluentAssertions;
using NSubstitute;

public class EntityTests
{
    [Fact]
    public void EqualsShouldReturnTrueWhenComparingTwoEqualEntities()
    {
        // Arrange
        Guid entityId = Guid.NewGuid();
        Entity<Guid>? entity1 = Substitute.For<Entity<Guid>>();
        Entity<Guid>? entity2 = Substitute.For<Entity<Guid>>();

        // Act
        bool result = entity1.Equals(entity2);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void EqualsShouldReturnFalseWhenComparingTwoDifferentEntities()
    {
        // Arrange
        Entity<int>? entity1 = Substitute.For<Entity<int>>();
        Entity<Guid>? entity2 = Substitute.For<Entity<Guid>>();

        // Act
        bool result = entity1.Equals(entity2);

        // Assert
        result.Should().BeFalse();
    }
}
