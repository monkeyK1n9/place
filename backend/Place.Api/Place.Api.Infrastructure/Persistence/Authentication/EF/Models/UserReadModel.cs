namespace Place.Api.Infrastructure.Persistence.Authentication.EF.Models;

using System;
using Domain.Common.Abstractions;

/// <summary>
/// Represents a read-only model of a user for querying purposes.
/// </summary>
/// <remarks>
/// This model includes user details and implements audit and soft delete functionality.
/// It should be used only for read operations to ensure data integrity.
/// </remarks>
internal class UserReadModel : IAuditableEntity, ISoftDeletableEntity
{
    /// <summary>
    /// Gets or sets the unique identifier for the user.
    /// </summary>
    public Ulid Id { get; set; }

    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown when the username is null or empty.</exception>
    public string UserName
    {
        get => this.userName;
        set =>
            this.userName = !string.IsNullOrWhiteSpace(value) ? value
            : throw new ArgumentNullException(nameof(this.UserName), "Username must not be null or empty.");
    }
    private string userName = null!;

    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown when the email is null or empty.</exception>
    public string Email
    {
        get => this.email;
        set =>
            this.email = !string.IsNullOrWhiteSpace(value) ? value
            : throw new ArgumentNullException(nameof(this.Email), "Email must not be null or empty.");
    }
    private string email = null!;

    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user's email is confirmed.
    /// </summary>
    public bool EmailIsConfirmed { get; set; }

    /// <summary>
    /// Gets or sets the password hash for the user.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown when the password hash is null or empty.</exception>
    public string PasswordHash
    {
        get => this.passwordHash;
        set =>
            this.passwordHash = !string.IsNullOrWhiteSpace(value) ? value
            : throw new ArgumentNullException(nameof(this.PasswordHash), "Password hash must not be null or empty.");
    }
    private string passwordHash = null!;

    /// <summary>
    /// Gets or sets the UTC date and time when the user was created.
    /// </summary>
    public DateTime CreatedOnUtc { get; set; }

    /// <summary>
    /// Gets or sets the UTC date and time when the user was last modified.
    /// </summary>
    public DateTime? ModifiedOnUtc { get; set; }

    /// <summary>
    /// Gets or sets the UTC date and time when the user was deleted.
    /// </summary>
    public DateTime? DeletedOnUtc { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user is deleted.
    /// </summary>
    public bool Deleted { get; set; }
}

