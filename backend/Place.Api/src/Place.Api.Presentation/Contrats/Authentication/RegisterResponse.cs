namespace Place.Api.Presentation.Contrats.Authentication;

public sealed record RegisterResponse(
    Ulid Id,
    string Login,
    string Username,
    bool EmailIsConfirmed
);
