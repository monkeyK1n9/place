namespace Place.Api.Presentation.Contrats.Authentication;

public sealed record RegisterResponse(
    string Id,
    string Login,
    string Username,
    bool EmailIsConfirmed
);
