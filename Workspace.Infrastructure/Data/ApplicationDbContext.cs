using Microsoft.EntityFrameworkCore;
using Workspace.Domain.Entities;

namespace Workspace.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);

            base.OnModelCreating(modelBuilder);
        }
    }
}