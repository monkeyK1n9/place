namespace Place.Api.Domain.Authentication;

using System.Text;
using Common.Abstractions;

public sealed class User : AggregateRoot<UserId, Ulid>
{
    private string passwordHash;
    public UserName UserName { get; private set; }
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Email Email { get; private set; }

    public string FullName
        => this.GetFullName(this.FirstName, this.LastName);

    private string GetFullName(FirstName firstName, LastName lastName)
    {
        StringBuilder builder = new();
        builder.Append(firstName)
            .Append("")
            .Append(lastName);

        return builder.ToString();
    }
}
