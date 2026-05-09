using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Bff.Application.Services;

namespace VeterinaryClinic.Bff.Api.Controllers;

[ApiController]
[Route("bff/v1/[controller]")]
[ApiVersion("1.0")]
[Authorize]
public class AuthController : ControllerBase
{
    private readonly IBffAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IBffAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var response = await _authService.LoginAsync(request, cancellationToken);

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout(CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst("sub")?.Value ?? User.Identity?.Name ?? "0");
        var response = await _authService.LogoutAsync(userId, cancellationToken);
        return Ok(response);
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser(CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst("sub")?.Value ?? User.Identity?.Name ?? "0");
        var response = await _authService.GetCurrentUserAsync(userId, cancellationToken);

        if (!response.Success)
        {
            return NotFound(response);
        }

        return Ok(response);
    }

    [HttpGet("navigation")]
    [Authorize]
    public async Task<IActionResult> GetNavigationMenu(CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst("sub")?.Value ?? User.Identity?.Name ?? "0");
        var response = await _authService.GetNavigationMenuAsync(userId, cancellationToken);
        return Ok(response);
    }
}

public class LoginRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}