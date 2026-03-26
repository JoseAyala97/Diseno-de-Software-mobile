using ExerciseTracker.Application.Common.Interfaces;

namespace ExerciseTracker.Application.Authentication;

public sealed class LoginUserUseCase
{
    private readonly IIdentityService _identityService;

    public LoginUserUseCase(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public Task<AuthResult> ExecuteAsync(LoginUserRequest request, CancellationToken cancellationToken = default)
        => _identityService.LoginAsync(request.Email, request.Password, request.RememberMe, cancellationToken);
}
