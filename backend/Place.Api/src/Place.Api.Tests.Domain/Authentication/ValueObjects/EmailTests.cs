namespace Place.Api.Tests.Domain.Authentication.ValueObjects;

using System.Text;
using Api.Domain.Authentication;
using Api.Domain.Authentication.ValueObjects;
using ErrorOr;
using FluentAssertions;
using Xunit;

public class EmailTests
{
    [Fact]
    public void CreateWithNullValueReturnsError()
    {
        // Arrange
        string? value = null;

        // Act
        ErrorOr<Email> result = Email.Create(value!);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(DomainErrors.Email.NullOrEmpty);
    }

    [Fact]
    public void CreateWithEmptyValueReturnsError()
    {
        // Arrange
        string value = string.Empty;

        // Act
        ErrorOr<Email> result = Email.Create(value);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(DomainErrors.Email.NullOrEmpty);
    }

    [Fact]
    public void CreateWithWhiteSpaceValueReturnsError()
    {
        // Arrange
        string value = " ";

        // Act
        ErrorOr<Email> result = Email.Create(value);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(DomainErrors.Email.NullOrEmpty);
    }

    [Fact]
    public void CreateWhenValueIsInvalidReturnsError()
    {
        // Arrange
        string value = "invalid-email-address";

        // Act
        ErrorOr<Email> result = Email.Create(value);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(DomainErrors.Email.InvalidFormat);
    }

    [Fact]
    public void CreateWithLongerValueThanAllowedReturnsError()
    {
        // Arrange
        StringBuilder builder = new();

        builder.Append("test")
            .Append(new string('a', Email.MaxLength + 1))
            .Append("@gmail.com");
        string value = builder.ToString();

        // Act
        ErrorOr<Email> result = Email.Create(value);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(DomainErrors.Email.LongerThanAllowed);
    }

    [Fact]
    public void CreateWithValidValueReturnsEmail()
    {
        // Arrange
        string value = "test@example.com";

        // Act
        ErrorOr<Email> result = Email.Create(value);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Value.Should().Be(value);
    }

    [Fact]
    public void ToStringReturnsValue()
    {
        // Arrange
        string value = "test@example.com";
        ErrorOr<Email> email = Email.Create(value);

        // Act
        string result = email.Value.ToString();

        // Assert
        result.Should().Be(value);
    }
}
