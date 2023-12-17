namespace Place.Api.Application.Common.Errors;

using ErrorOr;

public static partial class Errors
{
#pragma warning disable CA1034
    public static class Authentication
    {
        public static Error InvalidCredentials => Error.Validation(
            code: "Auth.InvalidCred",
            description: "Invalid credentials.");
    }
#pragma warning restore CA1034

}
