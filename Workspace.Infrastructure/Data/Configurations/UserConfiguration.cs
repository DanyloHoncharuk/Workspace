using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workspace.Domain.Entities;

namespace Workspace.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Login).IsRequired().HasMaxLength(50);
            builder.HasIndex(u => u.Login).IsUnique();
            builder.Property(u => u.PasswordHash).IsRequired();
            builder.Property(u => u.Name).IsRequired().HasMaxLength(50);
            builder.Property(u => u.Surname).IsRequired().HasMaxLength(50);
            builder.Property(u => u.Email).HasMaxLength(100);
            builder.Property(u => u.CreatedAt).IsRequired();
            builder.Property(u => u.UpdatedAt).IsRequired();
            builder.Property(u => u.IsDeleted).IsRequired();
            builder.Property(u => u.DeletedAt).IsRequired(false);
        }
    }
}