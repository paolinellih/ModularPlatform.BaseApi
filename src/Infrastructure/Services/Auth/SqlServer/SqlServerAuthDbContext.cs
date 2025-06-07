using Infrastructure.Interfaces.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.Services.Auth.Adapters;

namespace Infrastructure.Services.Auth.SqlServer;
public class SqlServerAuthDbContext : IdentityDbContext<IdentityAppUser>
{
    public SqlServerAuthDbContext(DbContextOptions<SqlServerAuthDbContext> options)
        : base(options)
    {
    }
}
