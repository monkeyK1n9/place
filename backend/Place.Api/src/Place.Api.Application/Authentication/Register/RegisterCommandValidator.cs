namespace Place.Api.Application.Authentication.Register;

using FluentValidation;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        this.RuleFor(x => x.Email).NotEmpty().EmailAddress();
        this.RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
        this.RuleFor(x => x.ConfirmPassword).NotEmpty().Equal(x => x.Password);
    }

}
