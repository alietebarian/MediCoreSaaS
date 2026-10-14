using Application.Abstractions.Identity;
using MediatR;

namespace Application.Identity.Login;
public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, bool>
{
    private readonly IIdentityService _identityService;

    public LoginCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<bool> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.ValidateCredentialsAsync(
            request.Email,
            request.Password,
            cancellationToken
        );
    }
}
