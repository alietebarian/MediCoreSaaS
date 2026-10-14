using Application.Abstractions.Identity;
using MediatR;

namespace Application.Identity.Register;

public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
{
    private readonly IIdentityService _identityService;

    public RegisterCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var emailExists = await _identityService.EmailExistsAsync(request.Email, cancellationToken);

        if (emailExists)
        {
            throw new InvalidOperationException(
                "An account with this email already exists.");
        }

        return await _identityService.RegisterAsync(
            request.Email,
            request.Password,
            request.FullName,
            cancellationToken
        );
    }
}
