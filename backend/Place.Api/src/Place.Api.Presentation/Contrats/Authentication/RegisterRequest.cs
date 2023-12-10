namespace Place.Api.Presentation.Contrats.Authentication;

public record RegisterRequest(
    string Email,
    string Password,
    string ConfirmPassword
);
