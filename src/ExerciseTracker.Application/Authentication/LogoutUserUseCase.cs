using ExerciseTracker.Application.Common.Interfaces;

namespace ExerciseTracker.Application.Authentication;

public sealed class LogoutUserUseCase
{
    private readonly IIdentityService _identityService;

    public LogoutUserUseCase(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public Task ExecuteAsync() => _identityService.LogoutAsync();
}
