using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Modules.Auth.Application.Requests;
using Modules.Auth.Application.Responses;
using Modules.Auth.Application.Interfaces;
using Modules.Logging.Application.Interfaces;

namespace Modules.Auth.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly ILoggingService<AuthService> _logger;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _config;

    public AuthService(ILoggingService<AuthService> logger, UserManager<IdentityUser> userManager, IConfiguration config)
    {
        _userManager = userManager;
        _config = config;
        _logger = logger;
    }

    public async Task<AuthResult> RegisterAsync(RegisterRequest request)
    {
        _logger.LogInfo($"Attempting to register user {request.Email}");
        var user = new IdentityUser { UserName = request.Email, Email = request.Email };
        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            _logger.LogInfo($"User {request.Email} registered successfully.");
            return AuthResult.Success();
        }
            
        _logger.LogError($"Failed to register user {request.Email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        var errors = result.Errors.Select(e => new AuthError(e.Code, e.Description));
        return AuthResult.Failure(errors);
    }

    public async Task<string> LoginAsync(LoginRequest request)
    {
        _logger.LogInfo($"Attempting to register user {request.Email}");
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            _logger.LogWarning($"Login failed for user {request.Email}: Invalid credentials.");
            throw new UnauthorizedAccessException("Invalid credentials.");
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: creds
        );

        _logger.LogInfo($"User {request.Email} logged in successfully. Token generated.");
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<AuthResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            _logger.LogWarning($"Change password failed: User with ID {userId} not found.");
            return AuthResult.Failure(new[] { new AuthError("UserNotFound", "User not found.") });   
        }

        var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        if (result.Succeeded)
        {
            _logger.LogInfo($"Password changed successfully for user {user.Email}.");
            return AuthResult.Success();
        }
            
        _logger.LogError($"Failed to change password for user {user.Email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        var errors = result.Errors.Select(e => new AuthError(e.Code, e.Description));
        return AuthResult.Failure(errors);
    }

    public async Task<string> GeneratePasswordResetTokenAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            _logger.LogWarning($"Password reset token generation failed: User with email {email} not found.");
            throw new InvalidOperationException("User not found.");
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        if (string.IsNullOrEmpty(token))
        {
            _logger.LogError($"Failed to generate password reset token for user {email}.");
            throw new InvalidOperationException("Failed to generate password reset token.");
        }
        _logger.LogInfo($"Password reset token generated for user {email}.");
        return token;
    }

    public async Task<AuthResult> ResetPasswordAsync(string email, string token, string newPassword)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            _logger.LogWarning($"Password reset failed: User with email {email} not found.");
            return AuthResult.Failure(new[] { new AuthError("UserNotFound", "User not found.") });
        }

        var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
        if (result.Succeeded)
        {
            _logger.LogInfo($"Password reset successfully for user {email}.");
            return AuthResult.Success();
        }

        _logger.LogError($"Failed to reset password for user {email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        var errors = result.Errors.Select(e => new AuthError(e.Code, e.Description));
        return AuthResult.Failure(errors);
    }
}