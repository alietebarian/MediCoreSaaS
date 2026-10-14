using MediatR;

namespace Application.Identity.Register;

public sealed record RegisterCommand(
    string Email,
    string Password,
    string FullName
) : IRequest<string>;
