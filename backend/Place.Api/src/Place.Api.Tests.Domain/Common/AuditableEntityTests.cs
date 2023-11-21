namespace Place.Api.Tests.Domain.Common;

using Api.Domain.Common.Abstractions;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

public class AuditableEntityTests
{

    [Fact]
    public void ModifiedOnUtcShouldBeNullByDefault()
    {
        // Arrange
        IAuditableEntity? auditableEntity = Substitute.For<IAuditableEntity>();

        // Act
        DateTime? result = auditableEntity.ModifiedOnUtc;

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void ModifiedOnUtcShouldBeSetWhenEntityIsModified()
    {
        // Arrange
        IAuditableEntity? auditableEntity = Substitute.For<IAuditableEntity>();
        DateTime modifiedOnUtc = DateTime.UtcNow;

        // Act
        auditableEntity.ModifiedOnUtc.Returns(modifiedOnUtc);

        // Assert
        auditableEntity.ModifiedOnUtc.Should().Be(modifiedOnUtc);
    }

    [Fact]
    public void ModifiedOnUtcShouldBeNullAfterEntityIsCreated()
    {
        // Arrange
        IAuditableEntity? auditableEntity = Substitute.For<IAuditableEntity>();
        DateTime modifiedOnUtc = DateTime.UtcNow;

        // Act
        auditableEntity.ModifiedOnUtc.Returns(modifiedOnUtc);
        auditableEntity.ModifiedOnUtc.ReturnsNull();

        // Assert
        auditableEntity.ModifiedOnUtc.Should().BeNull();
    }
}
