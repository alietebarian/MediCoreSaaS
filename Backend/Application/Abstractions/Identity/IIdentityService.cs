namespace Application.Abstractions.Identity;

public interface IIdentityService
{
    Task<bool> EmailExistsAsync(
        string email, CancellationToken cancellationToken = default);

    Task<string> RegisterAsync(
        string Email,
        string Password,
        string FullName,
        CancellationToken cancellationToken = default);

    Task<AuthenticatedUser?> ValidateCredentialsAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default);
}
