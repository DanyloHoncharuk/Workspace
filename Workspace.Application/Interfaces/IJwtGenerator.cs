namespace Workspace.Application.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateToken(string userId, string login);
        string GenerateRefreshToken();

        
    }
}