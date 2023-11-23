namespace Place.Api.Domain.Authentication;

using Place.Api.Domain.Authentication.ValueObjects;

public sealed class UserBuilder
{
    public static UserBuilder User() => new();

    private UserName userName = null!;
    private Email email = null!;
    private string passwordHash = null!;
    private FirstName? firstName;
    private LastName? lastName;


    public UserBuilder WithUserName(UserName value)
    {
        this.userName = value;
        return this;
    }

    public UserBuilder WithFirstName(FirstName value)
    {
        this.firstName = value;
        return this;
    }

    public UserBuilder WithLastName(LastName value)
    {
        this.lastName = value;
        return this;
    }

    public UserBuilder WithEmail(Email value)
    {
        this.email = value;
        return this;
    }

    public UserBuilder WithPassword(string value)
    {
        this.passwordHash = value;
        return this;
    }

    public User Build() =>
        new(
            this.userName,
            this.email,
            this.passwordHash,
            this.firstName,
            this.lastName
        );
}
