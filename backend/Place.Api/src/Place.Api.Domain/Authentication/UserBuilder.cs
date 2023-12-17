namespace Place.Api.Domain.Authentication;

using Place.Api.Domain.Authentication.ValueObjects;

/// <summary>
/// Builder for creating instances of the <see cref="User"/> class.
/// </summary>
public sealed class UserBuilder
{
    private UserName userName;
    private Email email;
    private string password;
    private FirstName? firstName;
    private LastName? lastName;
    private UserId? userId;


    public UserBuilder(UserName userName, Email email, string passwordHash)
    {
        this.userName = userName;
        this.email = email;
        this.password = passwordHash;
    }


    public UserBuilder WithId(UserId id)
    {
        this.userId = id;
        return this;
    }

    /// <summary>
    /// Sets the <see cref="UserName"/> property and returns the current instance of <see cref="UserBuilder"/>.
    /// </summary>
    /// <param name="value">The <see cref="UserName"/> for the user.</param>
    /// <returns>The current instance of <see cref="UserBuilder"/>.</returns>
    public UserBuilder WithUserName(UserName value)
    {
        this.userName = value;
        return this;
    }

    /// <summary>
    /// Sets the <see cref="FirstName"/> property and returns the current instance of <see cref="UserBuilder"/>.
    /// </summary>
    /// <param name="value">The <see cref="FirstName"/> for the user.</param>
    /// <returns>The current instance of <see cref="UserBuilder"/>.</returns>
    public UserBuilder WithFirstName(FirstName value)
    {
        this.firstName = value;
        return this;
    }

    /// <summary>
    /// Sets the <see cref="LastName"/> property and returns the current instance of <see cref="UserBuilder"/>.
    /// </summary>
    /// <param name="value">The <see cref="LastName"/> for the user.</param>
    /// <returns>The current instance of <see cref="UserBuilder"/>.</returns>
    public UserBuilder WithLastName(LastName value)
    {
        this.lastName = value;
        return this;
    }

    /// <summary>
    /// Builds and returns a new instance of <see cref="User"/> with the properties set on the <see cref="UserBuilder"/> instance.
    /// </summary>
    /// <returns>A new instance of <see cref="User"/>.</returns>
    public User Build() =>
        new(
            this.userId?? UserId.CreateUnique(),
            this.userName,
            this.email,
            this.password,
            this.firstName,
            this.lastName
        );
}
