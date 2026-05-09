using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Bff.Application.Services;

namespace VeterinaryClinic.Bff.Api.Controllers;

[ApiController]
[Route("bff/v1/[controller]")]
[ApiVersion("1.0")]
public class AttendanceQueueController : ControllerBase
{
    private readonly IAttendanceQueueService _queueService;
    private readonly ILogger<AttendanceQueueController> _logger;

    public AttendanceQueueController(IAttendanceQueueService queueService, ILogger<AttendanceQueueController> logger)
    {
        _queueService = queueService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetQueue(CancellationToken cancellationToken)
    {
        var response = await _queueService.GetQueueAsync(cancellationToken);
        return Ok(response);
    }

    [HttpGet("monitor")]
    public async Task<IActionResult> GetMonitorQueue(CancellationToken cancellationToken)
    {
        var response = await _queueService.GetMonitorQueueAsync(cancellationToken);
        return Ok(response);
    }

    [HttpPatch("{id:guid}/call")]
    public async Task<IActionResult> CallPatient(Guid id, CancellationToken cancellationToken)
    {
        var response = await _queueService.CallPatientAsync(id, cancellationToken);
        return Ok(response);
    }

    [HttpPatch("{id:guid}/finish")]
    public async Task<IActionResult> FinishAttendance(Guid id, CancellationToken cancellationToken)
    {
        var response = await _queueService.FinishAttendanceAsync(id, cancellationToken);
        return Ok(response);
    }
}