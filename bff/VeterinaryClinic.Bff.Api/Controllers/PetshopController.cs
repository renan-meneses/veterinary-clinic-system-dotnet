using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Bff.Application.Services;

namespace VeterinaryClinic.Bff.Api.Controllers;

[ApiController]
[Route("bff/v1/[controller]")]
[ApiVersion("1.0")]
public class PetshopController : ControllerBase
{
    private readonly IPetshopService _petshopService;
    private readonly ILogger<PetshopController> _logger;

    public PetshopController(IPetshopService petshopService, ILogger<PetshopController> logger)
    {
        _petshopService = petshopService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAttendances(CancellationToken cancellationToken)
    {
        var response = await _petshopService.GetAttendancesAsync(cancellationToken);
        return Ok(response);
    }

    [HttpGet("board")]
    public async Task<IActionResult> GetPetshopBoard(CancellationToken cancellationToken)
    {
        var response = await _petshopService.GetPetshopBoardAsync(cancellationToken);
        return Ok(response);
    }

    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateStatusRequest request, CancellationToken cancellationToken)
    {
        var response = await _petshopService.UpdateAttendanceStatusAsync(id, request.Status, cancellationToken);
        return Ok(response);
    }
}

public class UpdateStatusRequest
{
    public Guid Status { get; set; }
}