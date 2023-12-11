namespace Place.Api.Presentation.Endpoints;

#pragma warning disable CA1034
public static class ApiRoutes
{
    public static class Register
    {
        public const string Endpoint = "regsiter";
        public const string Summary = "Registers a new user";
        public const string OperationId = "RegisterUser";
        public static readonly string[] Tags = { "Authentication", "Register" };
        public const string Description = "Allows users to create a new account by providing their registration details. Upon successful registration, the system will generate unique credentials, and the user will gain access to the platform's features";
        public const string SuccessMessage = "User created successfuly";
    }

}

#pragma warning restore CA1034
