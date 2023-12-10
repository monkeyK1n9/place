namespace Place.Api.Domain.Authentication;

using System;
using Place.Api.Domain.Authentication.ValueObjects;

/// <summary>
/// Builder for creating instances of the <see cref="User"/> class.
/// </summary>
public sealed class UserBuilder
{
    private UserName userName = null!;
    private FirstName? firstName;
    private LastName? lastName;

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
    /// <param name="email">The email address for the user.</param>
    /// <param name="passwordHash">The hashed password for the user.</param>
    /// <returns>A new instance of <see cref="User"/>.</returns>
    public User Build(Email email, string passwordHash)
    {
        ArgumentNullException.ThrowIfNull(this.userName);

        return new User(
            UserId.CreateUnique(),
            this.userName,
            email,
            passwordHash,
            this.firstName,
            this.lastName
        );
    }
}
