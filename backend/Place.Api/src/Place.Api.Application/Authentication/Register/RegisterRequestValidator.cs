namespace Place.Api.Application.Authentication.Register;

using FluentValidation;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        this.RuleFor(x => x.Email).NotEmpty().EmailAddress();
        this.RuleFor(x => x.Password).NotEmpty().Length(8);
        this.RuleFor(x => x.ConfirmPassword).NotEmpty().Equal(x => x.Password);
    }

}
