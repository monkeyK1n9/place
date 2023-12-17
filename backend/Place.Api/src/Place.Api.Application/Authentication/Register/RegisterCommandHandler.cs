namespace Place.Api.Application.Authentication.Register;

using System.Threading;
using System.Threading.Tasks;
using Common.Interfaces.Authentication;
using Domain.Authentication;
using Domain.Authentication.ValueObjects;
using ErrorOr;
using MediatR;

public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<RegisterResult>>
{
    private readonly IUserRepository userRepository;
    private readonly IPasswordHasher passwordHasher;

    public RegisterCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
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

        User user = new UserBuilder(username.Value, email.Value, passwordHash).Build();


        await this.userRepository.AddAsync(user).ConfigureAwait(true);

        return new RegisterResult(user);
    }
}
