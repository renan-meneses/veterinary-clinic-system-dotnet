using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using VeterinaryClinic.Domain.Dtos;
using VeterinaryClinic.Application.Commands.Auth;
using VeterinaryClinic.Application.Queries.Auth;

namespace VeterinaryClinic.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[ApiVersion("1.0")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IMediator mediator, ILogger<AuthController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new LoginCommand(request.Email, request.Password), cancellationToken);

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPost("refresh-token")]
    [AllowAnonymous]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new RefreshTokenCommand(request.RefreshToken), cancellationToken);

        if (!response.Success)
        {
            return Unauthorized(response);
        }

        return Ok(response);
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout(CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst("sub")?.Value ?? User.Identity?.Name ?? "0");
        var response = await _mediator.Send(new LogoutCommand(userId), cancellationToken);

        return Ok(response);
    }

    [HttpPost("forgot-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDto request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new ForgotPasswordCommand(request.Email), cancellationToken);
        return Ok(response);
    }

    [HttpPost("change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst("sub")?.Value ?? User.Identity?.Name ?? "0");
        var response = await _mediator.Send(new ChangePasswordCommand(userId, request.CurrentPassword, request.NewPassword), cancellationToken);

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser(CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst("sub")?.Value ?? User.Identity?.Name ?? "0");
        var response = await _mediator.Send(new GetCurrentUserCommand(userId), cancellationToken);

        if (!response.Success)
        {
            return NotFound(response);
        }

        return Ok(response);
    }
}

public class RefreshTokenRequestDto
{
    public string RefreshToken { get; set; } = string.Empty;
}

public class ForgotPasswordRequestDto
{
    public string Email { get; set; } = string.Empty;
}

public class ChangePasswordRequestDto
{
    public string CurrentPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}