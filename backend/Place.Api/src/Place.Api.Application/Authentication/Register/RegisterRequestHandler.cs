namespace Place.Api.Application.Authentication.Register;

using System.Threading;
using System.Threading.Tasks;
using Common.Interfaces.Authentication;
using Domain.Authentication;
using Domain.Authentication.ValueObjects;
using ErrorOr;
using MediatR;

public sealed class RegisterRequestHandler : IRequestHandler<RegisterCommand, ErrorOr<RegisterResult>>
{
    private readonly IUserRepository userRepository;
    private readonly IPasswordHasher passwordHasher;

    public RegisterRequestHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        this.userRepository = userRepository;
        this.passwordHasher = passwordHasher;
    }

    public async Task<ErrorOr<RegisterResult>> Handle(
        RegisterCommand request,
        CancellationToken cancellationToken
    )
    {
        ErrorOr<Email> email = Email.Create(request.Email);

        if (email.IsError)
        {
            return email.FirstError;
        }

        bool isEmailExist = await this.userRepository
            .IsUniqueEmail(email.Value)
            .ConfigureAwait(false);

        if (isEmailExist)
        {
            return DomainErrors.User.DuplicateEmail;
        }

        ErrorOr<UserName> username = UserName.Create(email.Value);
        ErrorOr<Password> password = Password.Create(request.Password);

        if (password.IsError)
        {
            return password.FirstError;
        }
        string passwordHash = this.passwordHasher.HashPassword(password.Value);

        UserBuilder builder = new UserBuilder();

        builder.WithUserName(username.Value);
        User user = builder.Build(email.Value, passwordHash);

        await this.userRepository.AddAsync(user).ConfigureAwait(true);

        return new RegisterResult(user);
    }
}
