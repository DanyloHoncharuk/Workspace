namespace Workspace.Application.Common
{

    public enum ErrorType
    {
        NotFound,
        Validation,
        Unauthorized,
        Forbidden,
        Conflict,
        InternalServerError
    }
    public record Error(string Code, string Description = "", ErrorType Type = ErrorType.InternalServerError);
}