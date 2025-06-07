using Infrastructure.Interfaces.Auth;
using Infrastructure.Services.Auth.Adapters;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Auth.PostgreSQL;
public class PostgresAuthDbContext : IdentityDbContext<IdentityAppUser>, IAuthDbContext
{
    public PostgresAuthDbContext(DbContextOptions<PostgresAuthDbContext> options)
        : base(options) { }

    // Example: public DbSet<AppUser> Users => Set<AppUser>();
}

