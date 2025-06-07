using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services.Auth.Adapters;
public class IdentityAppUser : IdentityUser
{
    public AppUser ToDomain()
    {
        return new AppUser
        {
            Id = this.Id,
            Email = this.Email,
            PasswordHash = this.PasswordHash
        };
    }
    public static IdentityAppUser FromDomain(AppUser user)
    {
        return new IdentityAppUser
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.Email,
            PasswordHash = user.PasswordHash
        };
    }
}