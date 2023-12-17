namespace Place.Api.Application.Authentication.Login;

using System.Threading;
using System.Threading.Tasks;
using Common.Errors;
using Common.Interfaces.Authentication;
using Domain.Authentication;
using Domain.Authentication.ValueObjects;
using Domain.Services;
using MediatR;
using ErrorOr;
using Microsoft.Extensions.Logging;

internal sealed class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<LoginResult>>
{
    private readonly IUserRepository userRepository;
    private readonly IPasswordHashChecker passwordHashChecker;
    private readonly ILogger<LoginQueryHandler> logger;
    private readonly IJwTokenGenerator jwTokenGenerator;

    public LoginQueryHandler(
        IUserRepository userRepository,
        ILogger<LoginQueryHandler> logger,
        IPasswordHashChecker passwordHashChecker,
        IJwTokenGenerator jwTokenGenerator
        )
    {
        this.userRepository = userRepository;
        this.logger = logger;
        this.passwordHashChecker = passwordHashChecker;
        this.jwTokenGenerator = jwTokenGenerator;
    }

    public async Task<ErrorOr<LoginResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {

        User? user = await this.userRepository
            .GetByEmail(request.Email)
            .ConfigureAwait(false);

        if (user is null)
        {
            return Errors.Authentication.InvalidCredentials;
        }


        if (!user.VerifyPasswordHash(request.Password, this.passwordHashChecker))
        {
            return Errors.Authentication.InvalidCredentials;
        }

        string token = this.jwTokenGenerator.GenerateToken(user);

        return new LoginResult(user.Email.Value, user.UserName.Value, token);
    }
}
