namespace Place.Api.Application.Authentication.Register;

using System.Diagnostics;
using Domain.Authentication;

public record RegisterResponse
{
    public string Login { get; } = null!;
    public string? FirstName { get; }
    public string? LastName { get; }
    public bool EmailConfirmed { get; }
    public string UserName { get; } = null!;

    public RegisterResponse(User user)
    {
        Debug.Assert(user != null!, nameof(user) + " != null");
        this.Login = user.Email.Value;
        this.FirstName = user.FirstName?.Value;
        this.LastName = user.LastName?.Value;
        this.EmailConfirmed = user.EmailIsConfirmed;
        this.UserName = user.UserName.Value;
    }
}
