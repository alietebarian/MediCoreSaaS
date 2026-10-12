using Application.Abstractions.Identity;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Identity;

public sealed class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(email);

        return user is not null;
    }

    public async Task<string> RegisterAsync(string Email, string Password, string FullName, CancellationToken cancellationToken = default)
    {
        var user = new ApplicationUser
        {
            UserName = Email,
            Email = Email,
            FullName = FullName,
        };

        var result = await _userManager.CreateAsync(user, Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(
                ", ",
                result.Errors.Select(error => error.Description));

            throw new InvalidOperationException(
                $"User registration failed: {errors}");
        }

        return user.Id.ToString();
    }

    public async Task<bool> ValidateCredentialsAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null) return false;

        return await _userManager.CheckPasswordAsync(user, password);
    }
}
