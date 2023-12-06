namespace Place.Api.Application.Authentication.Register;

using ErrorOr;
using MediatR;

public record RegisterRequest(
    string Email,
    string Password,
    string ConfirmPassword) : IRequest<ErrorOr<RegisterResponse>>
    ;
