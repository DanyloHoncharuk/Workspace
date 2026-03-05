namespace Workspace.Application.Interfaces
{
    public interface IJwtGenerator
    {
        string GenerateToken(string userId, string login);
    }
}