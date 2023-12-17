namespace Place.Api.Presentation.Controllers;

using System.Net.Mime;
using Application.Authentication.Register;
using Application.Authentication.ForgotPassword;
using Contrats.Authentication;
using Endpoints;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Place.Api.Application;

public class AuthenticationController(ISender sender, IMapper mapper) : ApiController
{

    [HttpPost(ApiRoutes.Register.Endpoint)]
    [SwaggerOperation(
        Summary = ApiRoutes.Register.Summary,
        Description = ApiRoutes.Register.Description,
        OperationId = ApiRoutes.Register.OperationId
    )]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [SwaggerResponse(StatusCodes.Status201Created, ApiRoutes.Register.SuccessMessage, typeof(RegisterResponse))]
    public async Task<IActionResult> Register(RegisterRequest request, CancellationToken cancellationToken
    )
    {
        RegisterCommand command = mapper.Map<RegisterCommand>(request);

        ErrorOr<RegisterResult> result = await sender
            .Send(command, cancellationToken)
            .ConfigureAwait(true);

        return result.Match(
            source => this.Created(mapper.Map<RegisterResponse>(source)),
            errors => this.Problem(errors)
        );
    }

    [HttpPost(ApiRoutes.ForgotPassword.Endpoint)]
    [SwaggerOperation(
        Summary = ApiRoutes.ForgotPassword.Summary,
        Description = ApiRoutes.ForgotPassword.Description,
    )]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [SwaggerResponse(StatusCodes.Status201Created, ApiRoutes.ForgotPassword.SuccessMessage, typeof(SentOTPResponse))]
    public async Task<IActionResult> SendOTP(SendOTPRequest request, CancellationToken cancellationToken
    )
    {
        ForgotPasswordCommand command = mapper.Map<ForgotPasswordCommand>(request);

        ErrorOr<SendOTPResult> result = await sender
            .Send(command, cancellationToken)
            .ConfigureAwait(true);

        return result.Match(
            source => this.Created(mapper.Map<SentOTPResponse>(source)),
            errors => this.Problem(errors)
        );
    }
}
