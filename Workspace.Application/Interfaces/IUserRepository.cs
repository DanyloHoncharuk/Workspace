using Workspace.Domain.Entities;

namespace Workspace.Application.Interfaces
{
    public interface IUserRepository
    {
        // User-related data access methods
        Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<User?> GetUserByLoginAsync(string login, CancellationToken cancellationToken = default);
        void AddUser(User user);

        // RefreshToken-related data access methods
        Task<RefreshToken?> GetRefreshTokenByTokenAsync(string token, CancellationToken cancellationToken = default);
        void AddRefreshToken(RefreshToken refreshToken);
    }
}