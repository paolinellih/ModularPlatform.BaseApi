namespace Modules.Auth.Application.Responses;

public class AuthResult
{
    public bool Succeeded { get; }
    public IEnumerable<AuthError> Errors { get; }
    public static AuthResult Success() => new AuthResult(true);
    public static AuthResult Failure(IEnumerable<AuthError> errors) => new AuthResult(false, errors);

    private AuthResult(bool succeeded, IEnumerable<AuthError>? errors = null)
    {
        Succeeded = succeeded;
        Errors = errors ?? Enumerable.Empty<AuthError>();
    }
}

public class AuthError
{
    public string Code { get; }
    public string Description { get; }

    public AuthError(string code, string description) => (Code, Description) = ( code, description);
}