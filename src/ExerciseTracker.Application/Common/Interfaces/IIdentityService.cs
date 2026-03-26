namespace ExerciseTracker.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<AuthResult> RegisterAsync(string fullName, string email, string password, CancellationToken cancellationToken = default);
    Task<AuthResult> LoginAsync(string email, string password, bool rememberMe, CancellationToken cancellationToken = default);
    Task LogoutAsync();
}
