using Workspace.Application.Interfaces;
using Workspace.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Workspace.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        // User-related data access methods
        public async Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<User?> GetUserByLoginAsync(string login, CancellationToken cancellationToken = default)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Login == login, cancellationToken);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
        }

        // RefreshToken-related data access methods
        public async Task<RefreshToken?> GetRefreshTokenByTokenAsync(string token, CancellationToken cancellationToken = default)
        {
            return await _context.RefreshTokens
                .Include(rt => rt.User) // Related user
                .FirstOrDefaultAsync(cancellationToken);
        }

        public void AddRefreshToken(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Add(refreshToken);
        }
    }
}