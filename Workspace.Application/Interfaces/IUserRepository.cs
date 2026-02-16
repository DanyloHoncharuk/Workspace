using Workspace.Domain.Entities;

namespace Workspace.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(Guid userId);
        Task<User?> GetUserByLoginAsync(string login);
        void CreateUser(User user);
        void UpdateUser(User user);
    }
}