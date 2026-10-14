
namespace Application.Abstractions.Identity;

public sealed record AuthenticatedUser(
    Guid UserId,
    string Email,
    string FullName,
    IReadOnlyCollection<string> Roles);
