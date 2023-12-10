namespace Place.Api.Application.Authentication.Register;

using System;
using Domain.Authentication;

public record RegisterResult
{
    public string Login { get; } = null!;
    public bool EmailIsConfirmed { get; }
    public string Username { get; } = null!;
    public Ulid Id { get; set; }

    public RegisterResult(User user)
    {
        this.Id = user.Id.Value;
        this.Login = user.Email.Value;
        this.Username = user.UserName.Value;
        this.EmailIsConfirmed = user.EmailIsConfirmed;
    }
}
