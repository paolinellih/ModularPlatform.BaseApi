using Modules.Auth.Application.Requests;
using Modules.Auth.Application.Responses;

namespace Modules.Auth.Application.Interfaces;

public interface IAuthService
{
    Task<string> LoginAsync(LoginRequest request);
    Task<AuthResult> RegisterAsync(RegisterRequest request);
    Task<AuthResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
    Task<string> GeneratePasswordResetTokenAsync(string email);
    Task<AuthResult> ResetPasswordAsync(string email, string token, string newPassword);
}