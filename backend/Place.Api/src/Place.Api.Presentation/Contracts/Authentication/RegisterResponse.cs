namespace Place.Api.Presentation.Contracts.Authentication;

public sealed record RegisterResponse(
    string Id,
    string Login,
    string Username,
    bool EmailIsConfirmed
);
