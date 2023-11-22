namespace Place.Api.Tests.Domain.Authentication.ValueObjects;

using Api.Domain.Authentication;
using Api.Domain.Authentication.ValueObjects;
using ErrorOr;
using FluentAssertions;
using Xunit;


public class LastNameTests
{
    private const int MaxLength = 256;
    private const int MinLength = 3;


    [Fact]
    public void CreateWhenValueIsNullReturnsError()
    {
        // Arrange
        string? value = null;

        // Act
        ErrorOr<LastName> result = LastName.Create(value!);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(DomainErrors.LastName.NullOrEmpty);
    }

    [Fact]
    public void CreateWhenValueIsTooLongReturnsError()
    {
        // Arrange
        string value = new('a', MaxLength + 1);

        // Act
        ErrorOr<LastName> result = LastName.Create(value);

        // Assert
        result.FirstError.Should().Be(DomainErrors.LastName.LongerThanAllowed);
    }

    [Fact]
    public void CreateWhenValueIsTooShortReturnsError()
    {
        // Arrange
        string value = new('a', MinLength - 1);

        // Act
        ErrorOr<LastName> result = LastName.Create(value);

        // Assert
        result.FirstError.Should().Be(DomainErrors.LastName.LowerThanAllowed);
    }

    [Fact]
    public void CreateWhenValueIsValidReturnsFirstName()
    {
        // Arrange
        string value = "Naruto";

        // Act
        ErrorOr<LastName> result = LastName.Create(value);

        // Assert
        result.Value.Value.Should().Be(value);
    }
}
