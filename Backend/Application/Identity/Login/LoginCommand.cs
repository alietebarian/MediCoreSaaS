using MediatR;

namespace Application.Identity.Login;

public sealed record LoginCommand(
    string Email,
    string Password
) : IRequest<bool>;
