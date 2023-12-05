namespace Place.Api.Domain.Authentication;

using System;
using Place.Api.Domain.Authentication.ValueObjects;

public sealed class UserBuilder(Email email, string passwordHash)
{
    private UserName userName = null!;
    private FirstName? firstName;
    private LastName? lastName;

    /// <summary>
    /// Sets the <see cref="UserName"/> property and returns the current instance of <see cref="UserBuilder"/>.
    /// </summary>
    public UserBuilder WithUserName(UserName value)
    {
        this.userName = value;
        return this;
    }

    /// <summary>
    /// Sets the <see cref="FirstName"/> property and returns the current instance of <see cref="UserBuilder"/>.
    /// </summary>
    public UserBuilder WithFirstName(FirstName value)
    {
        this.firstName = value;
        return this;
    }

    /// <summary>
    /// Sets the <see cref="LastName"/> property and returns the current instance of <see cref="UserBuilder"/>.
    /// </summary>
    public UserBuilder WithLastName(LastName value)
    {
        this.lastName = value;
        return this;
    }

    /// <summary>
    /// Returns a new instance of <see cref="User"/> with the properties set on the <see cref="UserBuilder"/> instance.
    /// </summary>
    public User Build()
    {
        ArgumentNullException.ThrowIfNull(this.userName);


        return new(
            this.userName,
            email,
            passwordHash,
            this.firstName,
            this.lastName
        );
    }
}
