using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services.Auth.SqlServer;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSqlServerAuth(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<SqlServerAuthDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("AuthConnection")));

        services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<SqlServerAuthDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}