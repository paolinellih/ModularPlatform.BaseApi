using Modules.Auth.Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Services.Auth.Mongo;
public class MongoAuthRepository : IAuthRepository
{
    // Example: private readonly IMongoCollection<AppUser> _users;

    public MongoAuthRepository(/* inject MongoDB deps */)
    {
        // Init Mongo collections
    }

    // Example method: GetUserAsync, AddUserAsync, etc.
    public Task<AppUser> GetUserByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task CreateUserAsync(AppUser user)
    {
        throw new NotImplementedException();
    }
}
