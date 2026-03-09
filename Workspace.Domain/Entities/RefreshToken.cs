using Workspace.Domain.Common;

namespace Workspace.Domain.Entities
{
    public class RefreshToken : BaseEntity
    {
        public Guid UserId { get; private set;}
        public User User { get; private set;} = null!; // Navigation property to User enemity
        public string Token { get; private set;}
        public DateTime ExpirationDate { get; private set;}
        public bool IsExpired => DateTime.UtcNow >= ExpirationDate;
        public DateTime? RevokedAt { get; private set;}
        public bool IsRevoked { get; private set;}

        private RefreshToken() {}

        public RefreshToken(Guid userId, string token, DateTime expirationDate)
        {
            UserId = userId;
            Token = token;
            ExpirationDate = expirationDate;
            IsRevoked = false;
        }

        public void Revoke()
        {
            IsRevoked = true;
            RevokedAt = DateTime.UtcNow;
        }
    }
}
