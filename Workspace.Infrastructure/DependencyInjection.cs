using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Workspace.Application.Interfaces;
using Workspace.Infrastructure.Data;
using Workspace.Infrastructure.Services;


namespace Workspace.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
        
        services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();

        return services;
    }
}