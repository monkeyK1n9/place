namespace Place.Api.Presentation.Contracts.Authentication;

public record LoginResponse(string Email, string Username, string Token);

