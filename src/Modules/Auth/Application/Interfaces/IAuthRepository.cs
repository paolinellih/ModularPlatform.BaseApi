using Domain.Entities;

namespace Modules.Auth.Application.Interfaces;

public interface IAuthRepository
    {
        Task<AppUser> GetUserByEmailAsync(string email);
        Task CreateUserAsync(AppUser user);
        // Add other auth-related repository methods you need
    }