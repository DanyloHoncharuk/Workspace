using Workspace.Domain.Common;

namespace Workspace.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Login { get; private set; }
        public string PasswordHash { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string? Email { get; private set; }

        private User() {}
        public User(string login, string passwordHash, string name, string surname, string? email = null)
            : base()
        {
            Login = login;
            PasswordHash = passwordHash;
            Name = name;
            Surname = surname;
            Email = email;
        }

        public void UpdateProfile(string name, string surname, string? email)
        {
            Name = name;
            Surname = surname;
            Email = email;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetPassword(string passwordHash)
        {
            PasswordHash = passwordHash;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}