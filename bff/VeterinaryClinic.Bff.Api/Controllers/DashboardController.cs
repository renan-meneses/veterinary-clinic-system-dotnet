using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Bff.Application.Services;

namespace VeterinaryClinic.Bff.Api.Controllers;

[ApiController]
[Route("bff/v1/[controller]")]
[ApiVersion("1.0")]
[Authorize]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;
    private readonly ILogger<DashboardController> _logger;

    public DashboardController(IDashboardService dashboardService, ILogger<DashboardController> logger)
    {
        _dashboardService = dashboardService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetDashboard(CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst("sub")?.Value ?? User.Identity?.Name ?? "0");
        var response = await _dashboardService.GetDashboardAsync(userId, cancellationToken);
        return Ok(response);
    }

    [HttpGet("finance")]
    public async Task<IActionResult> GetFinanceDashboard(CancellationToken cancellationToken)
    {
        var response = await _dashboardService.GetFinanceDashboardAsync(cancellationToken);
        return Ok(response);
    }
}