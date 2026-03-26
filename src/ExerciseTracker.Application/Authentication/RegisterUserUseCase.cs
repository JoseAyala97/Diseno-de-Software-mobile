using ExerciseTracker.Application.Common.Interfaces;

namespace ExerciseTracker.Application.Authentication;

public sealed class RegisterUserUseCase
{
    private readonly IIdentityService _identityService;

    public RegisterUserUseCase(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public Task<AuthResult> ExecuteAsync(RegisterUserRequest request, CancellationToken cancellationToken = default)
        => _identityService.RegisterAsync(request.FullName, request.Email, request.Password, cancellationToken);
}
