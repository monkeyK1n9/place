namespace Place.Api.Tests.Domain.Authentication;

using FluentAssertions;
using Xunit;
using Place.Api.Domain.Authentication;
using Place.Api.Domain.Authentication.ValueObjects;

public class UserBuilderTests
{
    private readonly UserBuilder userBuilder;
    private readonly FirstName firstName;
    private readonly LastName lastName;
    private readonly UserName userName;
    private readonly Email email;
    private readonly string passwordHash;

    public UserBuilderTests()
    {
        this.userName = UserName.Create("@aldebaran").Value;
        this.firstName = FirstName.Create("Itachi").Value;
        this.lastName = LastName.Create("Uchiwa").Value;
        this.email = Email.Create("itachiuchiwa@gmail.com").Value;
        this.passwordHash = "hashedPassword";
        this.userBuilder = new UserBuilder(this.userName, this.email, this.passwordHash);
    }

    [Fact]
    public void WithUserNameShouldSetUserName()
    {
        UserBuilder result = this.userBuilder.WithUserName(this.userName);

        result.Should().NotBeNull();
        result.Should().BeOfType<UserBuilder>();
    }

    [Fact]
    public void WithFirstNameShouldSetFirstName()
    {
        UserBuilder result = this.userBuilder.WithFirstName(this.firstName);
        result.Should().NotBeNull();
        result.Should().BeOfType<UserBuilder>();
    }

    [Fact]
    public void WithLastNameShouldSetLastName()
    {
        UserBuilder result = this.userBuilder.WithLastName(this.lastName);

        result.Should().NotBeNull();
        result.Should().BeOfType<UserBuilder>();
    }

    [Fact]
    public void BuildShouldReturnUser()
    {
        this.userBuilder.WithUserName(this.userName);
        this.userBuilder.WithFirstName(this.firstName);
        this.userBuilder.WithLastName(this.lastName);

        User result = this.userBuilder.Build();

        result.Should().NotBeNull();
        result.Should().BeOfType<User>();
        result.FirstName!.Value.Should().Be(this.firstName.Value);
        result.LastName!.Value.Should().Be(this.lastName.Value);
        result.UserName.Value.Should().Be(this.userName.Value);
        result.Email!.Value.Should().Be(this.email.Value);
    }
}
