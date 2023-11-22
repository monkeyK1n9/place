namespace Place.Api.Domain.Authentication;

using System.Text;
using Common.Abstractions;
using ValueObjects;

public sealed class User : AggregateRoot<UserId, Ulid>
{
    //private string? passwordHash;

    public User(UserId id, UserName userName, FirstName firstName, LastName lastName, Email email) : base(id)
    {
        this.UserName = userName;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
    }

    public User(UserName userName, FirstName firstName, LastName lastName, Email email)
    {
        this.UserName = userName;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
    }

    public UserName UserName { get; private set; }
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Email Email { get; private set; }

    public string FullName
        => GetFullName(this.FirstName, this.LastName);

    private static string GetFullName(FirstName firstName, LastName lastName)
    {
        StringBuilder builder = new();
        builder.Append(firstName)
            .Append("")
            .Append(lastName);

        return builder.ToString();
    }
}
