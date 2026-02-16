using Workspace.Application.Common;

namespace Workspace.Application.Common.Errors
{
    public static partial class Errors
    {
        public static class User
        {
            public static readonly Error NotFound = new Error("User.NotFound", "User not found.");
            public static readonly Error InvalidCredentials = new Error("User.InvalidCredentials", "Invalid login or password.");
            public static readonly Error UserAlreadyExists = new Error("User.UserAlreadyExists", "A user with the same login already exists.");
        }
    }
}