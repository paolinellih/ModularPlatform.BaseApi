using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services.Auth.PostgreSQL;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPostgreSqlAuth(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<PostgresAuthDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("AuthConnection")));

        services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<PostgresAuthDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}