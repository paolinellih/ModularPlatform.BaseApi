namespace Modules.Auth.Application.Requests;
public class ForgotPasswordRequest
{
    public string Email { get; set; } = default!;
}