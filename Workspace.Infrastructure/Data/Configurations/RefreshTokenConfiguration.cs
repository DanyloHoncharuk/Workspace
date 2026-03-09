using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workspace.Domain.Entities;

namespace Workspace.Infrastructure.Data.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(rt => rt.Id);
            builder.Property(rt => rt.UserId).IsRequired();
            builder.Property(rt => rt.Token).IsRequired().HasMaxLength(255);
            builder.Property(rt => rt.ExpirationDate).IsRequired();
            builder.Property(rt => rt.IsRevoked).IsRequired();
            builder.Property(rt => rt.RevokedAt).IsRequired(false);

            builder.HasOne(rt => rt.User)
                   .WithMany()
                   .HasForeignKey(rt => rt.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}