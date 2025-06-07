namespace Modules.Auth.Application.Requests;
public class ChangePasswordRequest
{
    public string Email { get; set; } = default!;
    public string CurrentPassword { get; set; } = default!;
    public string NewPassword { get; set; } = default!;
}