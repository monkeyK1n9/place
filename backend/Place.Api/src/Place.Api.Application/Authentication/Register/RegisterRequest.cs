namespace Place.Api.Application.Authentication.Register;

using ErrorOr;
using MediatR;

public record RegisterCommand(
    string Email,
    string Password,
    string ConfirmPassword) : IRequest<ErrorOr<RegisterResult>>
    ;
