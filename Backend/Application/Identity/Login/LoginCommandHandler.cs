
using Application.Abstractions.Authentication;
using Application.Abstractions.Identity;
using MediatR;

namespace Application.Identity.Login;

public sealed class LoginCommandHandler
    : IRequestHandler<LoginCommand, LoginResponse?>
{
    private readonly IIdentityService _identityService;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginCommandHandler(
        IIdentityService identityService,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _identityService = identityService;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<LoginResponse?> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _identityService.ValidateCredentialsAsync(
            request.Email,
            request.Password,
            cancellationToken);

        if (user is null)
        {
            return null;
        }

        var accessToken = _jwtTokenGenerator.GenerateToken(
            user.UserId,
            user.Email,
            user.FullName,
            user.Roles);

        return new LoginResponse(
            accessToken,
            user.UserId,
            user.Email,
            user.FullName,
            user.Roles);
    }
}
