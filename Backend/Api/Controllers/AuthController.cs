using Application.Identity.Login;
using Application.Identity.Register;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/auth")]
[ApiController]
public sealed class AuthController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IValidator<RegisterCommand> _RegisterValidator;
    private readonly IValidator<LoginCommand> _LoginValidator;

    public AuthController(IValidator<LoginCommand> loginValidator, IValidator<RegisterCommand> registerValidator, ISender sender)
    {
        _LoginValidator = loginValidator;
        _RegisterValidator = registerValidator;
        _sender = sender;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _RegisterValidator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(error => new
            {
                error.PropertyName,
                error.ErrorMessage
            }));
        }

        try
        {
            var userId = await _sender.Send(command, cancellationToken);

            return Ok(new
            {
                UserId = userId,
                Message = "Registration successful."
            });
        }
        catch (InvalidOperationException exception)
        {
            return BadRequest(new
            {
                Message = exception.Message
            });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult =
            await _LoginValidator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(error => new
            {
                error.PropertyName,
                error.ErrorMessage
            }));
        }

        var isValid = await _sender.Send(command, cancellationToken);

        if (!isValid)
        {
            return Unauthorized(new
            {
                Message = "Invalid email or password."
            });
        }

        return Ok(new
        {
            Message = "Credentials are valid."
        });
    }
}
