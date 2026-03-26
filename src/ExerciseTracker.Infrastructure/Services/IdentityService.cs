using ExerciseTracker.Application.Common.Interfaces;
using ExerciseTracker.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExerciseTracker.Infrastructure.Services;

public sealed class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public IdentityService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<AuthResult> LoginAsync(string email, string password, bool rememberMe, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
        if (user is null)
        {
            return new AuthResult(false, ["El usuario no existe."]);
        }

        var result = await _signInManager.PasswordSignInAsync(user, password, rememberMe, lockoutOnFailure: false);
        return result.Succeeded
            ? new AuthResult(true, [], user.Id)
            : new AuthResult(false, ["Credenciales invalidas."]);
    }

    public async Task<AuthResult> RegisterAsync(string fullName, string email, string password, CancellationToken cancellationToken = default)
    {
        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            FullName = fullName
        };

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            return new AuthResult(false, result.Errors.Select(x => x.Description));
        }

        await _signInManager.SignInAsync(user, isPersistent: false);
        return new AuthResult(true, [], user.Id);
    }

    public Task LogoutAsync() => _signInManager.SignOutAsync();
}
