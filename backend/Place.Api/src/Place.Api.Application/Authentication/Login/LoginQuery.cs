namespace Place.Api.Application.Authentication.Login;

using ErrorOr;
using MediatR;

public record LoginQuery(string Email, string Password) : IRequest<ErrorOr<LoginResult>>;
