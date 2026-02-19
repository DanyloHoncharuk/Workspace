using Workspace.Domain.Entities;

namespace Workspace.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<User?> GetUserByLoginAsync(string login, CancellationToken cancellationToken = default);
        void CreateUser(User user);
        void UpdateUser(User user);
    }
}