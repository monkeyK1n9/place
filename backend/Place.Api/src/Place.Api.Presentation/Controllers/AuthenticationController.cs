namespace Place.Api.Presentation.Controllers;

using Application.Authentication.Register;
using Contrats.Authentication;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

public class AuthenticationController(ISender sender, IMapper mapper) : ApiController
{
    [HttpPost("register")]
    [Consumes("application/json")]
    public async Task<IActionResult> Register(RegisterRequest request, CancellationToken cancellationToken
    )
    {
        RegisterCommand command = mapper.Map<RegisterCommand>(request);

        ErrorOr<RegisterResult> result = await sender
            .Send(command, cancellationToken)
            .ConfigureAwait(true);

        return result.Match(
            source => this.Ok(mapper.Map<RegisterResponse>(source)),
            errors => this.Problem(errors)
        );
    }
}
