namespace Place.Api.Application.Authentication.Register;

using System;
using Domain.Authentication;

/// <summary>
/// Represents the result of a user registration operation.
/// </summary>
public record RegisterResult
{
    /// <summary>
    /// Gets the login name associated with the registered user.
    /// </summary>
    public string Login { get; } = null!;

    /// <summary>
    /// Gets a value indicating whether the user's email is confirmed.
    /// </summary>
    public bool EmailIsConfirmed { get; }

    /// <summary>
    /// Gets the username associated with the registered user.
    /// </summary>
    public string Username { get; } = null!;

    /// <summary>
    /// Gets or sets the unique identifier of the registered user.
    /// </summary>
    public Ulid Id { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RegisterResult"/> class based on a user entity.
    /// </summary>
    /// <param name="user">The user entity from which to create the result.</param>
    public RegisterResult(User user)
    {
        this.Id = user.Id.Value;
        this.Login = user.Email.Value;
        this.Username = user.UserName.Value;
        this.EmailIsConfirmed = user.EmailIsConfirmed;
    }
}
