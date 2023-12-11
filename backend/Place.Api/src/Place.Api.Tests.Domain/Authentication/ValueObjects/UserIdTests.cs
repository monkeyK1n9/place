namespace Place.Api.Tests.Domain.Authentication.ValueObjects;

using System;
using System.Collections.Generic;
using System.Linq;
using Api.Domain.Authentication.ValueObjects;
using FluentAssertions;

public class UserIdTests
{
    [Fact]
    public void CreateUniqueReturnsNewUserId()
    {
        // Act
        UserId result = UserId.CreateUnique();

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().NotBeNull();
    }

    [Fact]
    public void CreateWithValidValueReturnsUserId()
    {
        // Arrange
        Ulid value = Ulid.NewUlid();

        // Act
        UserId result = UserId.Create(value);

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().Be(value);
    }

    [Fact]
    public void GetEqualityComponentsReturnsValue()
    {
        // Arrange
        Ulid value = Ulid.NewUlid();
        UserId userId = UserId.Create(value);

        // Act
        IEnumerable<object> result = userId.GetEqualityComponents();

        // Assert
        IEnumerable<object> objects = result as object[] ?? result.ToArray();
        objects.Should().NotBeNull();
        objects.Should().Contain(value);
    }
}
