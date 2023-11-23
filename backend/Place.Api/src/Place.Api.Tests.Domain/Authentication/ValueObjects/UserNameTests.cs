namespace Place.Api.Tests.Domain.Authentication.ValueObjects;

using Api.Domain.Authentication;
using Api.Domain.Authentication.ValueObjects;
using ErrorOr;
using FluentAssertions;

public class UserNameTests
{
    private const int MaxLength = 12;
    private const int MinLength = 5;

    [Fact]
    public void CreateWithNullValueReturnsError()
    {
        // Arrange
        string? value = null;

        // Act
        ErrorOr<UserName> result = UserName.Create(value!);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(DomainErrors.UserName.NullOrEmpty);
    }

    [Fact]
    public void CreateWithTooLongValueReturnsError()
    {
        // Arrange
        string value = new('a', MaxLength + 1);

        // Act
        ErrorOr<UserName> result = UserName.Create(value);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(DomainErrors.UserName.LongerThanAllowed);
    }

    [Fact]
    public void CreateWithTooShortValueReturnsError()
    {
        // Arrange
        string value = new('a', MinLength - 1);

        // Act
        ErrorOr<UserName> result = UserName.Create(value);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(DomainErrors.UserName.LowerThanAllowed);
    }

    [Fact]
    public void CreateWithValidValueReturnsUserName()
    {
        // Arrange
        string value = "validname";

        // Act
        ErrorOr<UserName> result = UserName.Create(value);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Value.Should().Be(value);
    }


    [Fact]
    public void ToStringReturnsValue()
    {
        // Arrange
        string value = "Uchiwa";
        ErrorOr<UserName> email = UserName.Create(value);

        // Act
        string result = email.Value.ToString();

        // Assert
        result.Should().Be(value);
    }
}
