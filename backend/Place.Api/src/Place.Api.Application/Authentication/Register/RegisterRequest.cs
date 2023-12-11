namespace Place.Api.Application.Authentication.Register;

using ErrorOr;
using MediatR;

/// <summary>
/// Represents a command to register a new user.
/// </summary>
public record RegisterCommand(
    string Email,
    string Password,
    string ConfirmPassword) : IRequest<ErrorOr<RegisterResult>>;
