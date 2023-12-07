namespace Place.Api.Domain.Authentication;

using System;
using System.Text;
using Common.Abstractions;
using ErrorOr;
using Services;
using ValueObjects;

public sealed class User : AggregateRoot<UserId, Ulid>, IAuditableEntity, ISoftDeletableEntity
{
    private string passwordHash;


    internal User(UserId id,
        UserName userName,
        Email email,
        string passwordHash,
        FirstName? firstName,
        LastName? lastName)
        : base(id)
    {
        this.UserName = userName;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
        this.passwordHash = passwordHash;
    }

    internal User(
        UserName userName,
        Email email,
        string passwordHash,
        FirstName? firstName,
        LastName? lastName
    )
    {
        this.UserName = userName;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
        this.passwordHash = passwordHash;
        this.CreatedOnUtc = DateTime.UtcNow;
    }

#pragma warning disable CS8618
    private User()
    {
    }
#pragma warning restore CS8618

    public UserName UserName { get; private set; }

    public Email Email { get; private set; }
    public FirstName? FirstName { get; private set; }
    public LastName? LastName { get; private set; }

    public bool EmailIsConfirmed { get; private set; }

    public string FullName
        => GetFullName(this.FirstName, this.LastName);


    public bool VerifyPasswordHash(string password, IPasswordHashChecker passwordHashChecker)
#pragma warning disable CA1062
        => !string.IsNullOrWhiteSpace(password) && passwordHashChecker!.HashesMatch(this.passwordHash, password);
#pragma warning restore CA1062

    public ErrorOr<Success> ChangePassword(string hash)
    {
        if (this.passwordHash == hash)
        {
            return DomainErrors.User.CannotChangePassword;
        }

        this.passwordHash = hash;


        return Result.Success;
    }

    public void ChangeFirstName(FirstName? firstname)
        => this.FirstName = firstname;

    public void ChangeLastName(LastName? username)
        => this.LastName = username;

    public void ChangeUsername(UserName username)
        => this.UserName = username;

    public void ConfirmEmail() => this.EmailIsConfirmed = false;

    private static string GetFullName(FirstName? firstName, LastName? lastName)
    {
        StringBuilder builder = new();
        builder.Append(firstName)
            .Append("")
            .Append(lastName);

        return builder.ToString();
    }

    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; set; }
    public bool Deleted { get; set; }
    public DateTime? DeletedOnUtc { get; set; }

}
