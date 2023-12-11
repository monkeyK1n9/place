namespace Place.Api.Tests.Domain.Common;

using System.Collections.Generic;
using Api.Domain.Common.Abstractions;
using FluentAssertions;
using NSubstitute;

public class ValueObjectTests
{
    [Fact]
    public void EqualsShouldReturnTrueWhenComparingTwoEqualObjects()
    {
        // Arrange
        ValueObject? valueObject1 = Substitute.For<ValueObject>();
        ValueObject? valueObject2 = Substitute.For<ValueObject>();

        valueObject1.GetEqualityComponents().Returns(new List<object> { 1, "test" });
        valueObject2.GetEqualityComponents().Returns(new List<object> { 1, "test" });

        // Act
        bool result = valueObject1.Equals(valueObject2);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void EqualsShouldReturnFalseWhenComparingTwoDifferentObjects()
    {
        // Arrange
        ValueObject? valueObject1 = Substitute.For<ValueObject>();
        ValueObject? valueObject2 = Substitute.For<ValueObject>();

        valueObject1.GetEqualityComponents().Returns(new List<object> { 1, "test" });
        valueObject2.GetEqualityComponents().Returns(new List<object> { 2, "test" });

        // Act
        bool result = valueObject1.Equals(valueObject2);

        // Assert
        result.Should().BeFalse();
    }
}
