namespace Place.Api.Domain.Authentication;

using ErrorOr;

public static class DomainErrors
{
    internal static class Email
    {
        public static Error DuplicateEmail => Error.Conflict(
            code: "User.DuplicateEmail",
            description: "Email is already in use.");

        public static Error NullOrEmpty => Error.Validation(
            code: "Email.NullOrEmpty",
            description: "The email is required.");

        public static Error LongerThanAllowed => Error.Validation(
            code: "Email.LongerThanAllowed",
            description: "The email is longer than allowed.");
    }

    internal class FirstName
    {
        public static Error LongerThanAllowed => Error.Validation(
            code: "FirstName.LongerThanAllowed",
            description: "The first name is longer than allowed.");

        public static Error LowerThanAllowed => Error.Validation(
            code: "FirstName.LowerThanAllowed",
            description: "The first name is lower than allowed.");

        public static Error NullOrEmpty => Error.Validation(
            code: "FirstName.NullOrEmpty",
            description: "The first name is required.");
    }

    internal class LastName
    {
        public static Error LongerThanAllowed => Error.Validation(
            code: "LastName.LongerThanAllowed",
            description: "The last name is longer than allowed.");

        public static Error LowerThanAllowed => Error.Validation(
            code: "LastName.LowerThanAllowed",
            description: "The last name is lower than allowed.");

        public static Error NullOrEmpty => Error.Validation(
            code: "LastName.NullOrEmpty",
            description: "The last name is required.");
    }

    internal class UserName
    {
        public static Error LongerThanAllowed => Error.Validation(
            code: "UserName.LongerThanAllowed",
            description: "The username is longer than allowed.");

        public static Error LowerThanAllowed => Error.Validation(
            code: "UserName.LowerThanAllowed",
            description: "The username is lower than allowed.");

        public static Error NullOrEmpty => Error.Validation(
            code: "UserName.NullOrEmpty",
            description: "The username is required.");
    }
}
