namespace Place.Api.Application.Authentication.Login;

using FluentValidation;

public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        this.RuleFor(x => x.Email).EmailAddress().NotEmpty();
        this.RuleFor(x => x.Password).NotEmpty();
    }
}
