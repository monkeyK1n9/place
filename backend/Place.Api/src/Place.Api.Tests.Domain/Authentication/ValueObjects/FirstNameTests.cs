namespace Place.Api.Tests.Domain.Authentication.ValueObjects;

using Api.Domain.Authentication;
using Api.Domain.Authentication.ValueObjects;
using ErrorOr;
using FluentAssertions;
using Xunit;

public class FirstNameTests
{
    private const int MaxLength = 256;
    private const int MinLength = 3;


    [Fact]
    public void CreateWhenValueIsNullReturnsError()
    {
        // Arrange
        string? value = null;

        // Act
        ErrorOr<FirstName> result = FirstName.Create(value!);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(DomainErrors.FirstName.NullOrEmpty);
    }

    [Fact]
    public void CreateWhenValueIsTooLongReturnsError()
    {
        // Arrange
        string value = new('a', MaxLength + 1);

        // Act
        ErrorOr<FirstName> result = FirstName.Create(value);

        // Assert
        result.FirstError.Should().Be(DomainErrors.FirstName.LongerThanAllowed);
    }

    [Fact]
    public void CreateWhenValueIsTooShortReturnsError()
    {
        // Arrange
        string value = new('a', MinLength - 1);

        // Act
        ErrorOr<FirstName> result = FirstName.Create(value);

        // Assert
        result.FirstError.Should().Be(DomainErrors.FirstName.LowerThanAllowed);
    }

    [Fact]
    public void CreateWhenValueIsValidReturnsFirstName()
    {
        // Arrange
        string value = "John";

        // Act
        ErrorOr<FirstName> result = FirstName.Create(value);

        // Assert
        result.Value.Value.Should().Be(value);
    }

    [Fact]
    public void ToStringReturnsValue()
    {
        // Arrange
        string value = "Sanix Darker";
        ErrorOr<FirstName> email = FirstName.Create(value);

        // Act
        string result = email.Value.ToString();

        // Assert
        result.Should().Be(value);
    }
}
