namespace Modules.Auth.Application.Requests;
public class ResetPasswordRequest
{
    public string Email { get; set; } = default!;
    public string Token { get; set; } = default!;
    public string NewPassword { get; set; } = default!;
}