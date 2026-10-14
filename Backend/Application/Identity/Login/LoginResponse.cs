namespace Application.Identity.Login;

public sealed record LoginResponse(
    string AccessToken,
    Guid UserId,
    string Email,
    string FullName,
    IReadOnlyCollection<string> Roles);