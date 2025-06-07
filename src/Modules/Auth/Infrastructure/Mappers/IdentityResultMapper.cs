using Modules.Auth.Application.Responses;
using Microsoft.AspNetCore.Identity;

namespace Modules.Auth.Infrastructure.Mappers;

public static class IdentityResultMapper
{
    public static AuthResult ToAuthResult(this IdentityResult result)
    {
        if (result.Succeeded)
            return AuthResult.Success();

        var errors = result.Errors.Select(e => new AuthError(e.Code, e.Description));
        return AuthResult.Failure(errors.ToArray());
    }
}