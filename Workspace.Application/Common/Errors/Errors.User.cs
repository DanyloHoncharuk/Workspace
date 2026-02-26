namespace Workspace.Application.Common.Errors
{
    public static partial class Errors
    {
        public static class User
        {
            public static readonly Error NotFound = new Error("User.NotFound", "User not found.", ErrorType.NotFound);
            public static readonly Error InvalidCredentials = new Error("User.InvalidCredentials", "Invalid login or password.", ErrorType.Unauthorized);
            public static readonly Error UserAlreadyExists = new Error("User.UserAlreadyExists", "A user with the same login already exists.",ErrorType.Conflict);
            public static readonly Error LoginLength = new Error("User.LoginLength", "Login must be between 3 and 50 characters long.", ErrorType.Validation);
            public static readonly Error PasswordLength = new Error("User.PasswordLength", "Password must be between 8 and 30 characters long.", ErrorType.Validation);
            public static readonly Error InvalidEmailFormat = new Error("User.InvalidEmailFormat", "Email format is invalid.", ErrorType.Validation);
            public static readonly Error NameLength = new Error("User.NameLength", "Name must be at most 100 characters long.", ErrorType.Validation);
            public static readonly Error SurnameLength = new Error("User.SurnameLength", "Surname must be at most 100 characters long.", ErrorType.Validation);
            public static readonly Error InvalidNameFormat = new Error("User.InvalidNameFormat", "Name must start with an uppercase letter followed by lowercase letters.", ErrorType.Validation);
            public static readonly Error InvalidSurnameFormat = new Error("User.InvalidSurnameFormat", "Surname must start with an uppercase letter followed by lowercase letters.", ErrorType.Validation);
        }
    }
}