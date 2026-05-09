using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Bff.Application.Services;
using VeterinaryClinic.Bff.Domain.Models;

namespace VeterinaryClinic.Bff.Api.Controllers;

[ApiController]
[Route("bff/v1/[controller]")]
[ApiVersion("1.0")]
public class TutorController : ControllerBase
{
    private readonly ITutorPortalService _tutorService;
    private readonly ILogger<TutorController> _logger;

    public TutorController(ITutorPortalService tutorService, ILogger<TutorController> logger)
    {
        _tutorService = tutorService;
        _logger = logger;
    }

    [HttpGet("dashboard")]
    public async Task<IActionResult> GetTutorDashboard(CancellationToken cancellationToken)
    {
        var tutorId = GetTutorIdFromClaims();
        var response = await _tutorService.GetTutorDashboardAsync(tutorId, cancellationToken);
        return Ok(response);
    }

    [HttpGet("animals")]
    public async Task<IActionResult> GetTutorAnimals(CancellationToken cancellationToken)
    {
        var tutorId = GetTutorIdFromClaims();
        var response = await _tutorService.GetTutorAnimalsAsync(tutorId, cancellationToken);
        return Ok(response);
    }

    [HttpGet("vaccines")]
    public async Task<IActionResult> GetTutorVaccines(CancellationToken cancellationToken)
    {
        var tutorId = GetTutorIdFromClaims();
        var response = await _tutorService.GetTutorVaccinesAsync(tutorId, cancellationToken);
        return Ok(response);
    }

    [HttpGet("consultations")]
    public async Task<IActionResult> GetTutorConsultations(CancellationToken cancellationToken)
    {
        var tutorId = GetTutorIdFromClaims();
        var response = await _tutorService.GetTutorConsultationsAsync(tutorId, cancellationToken);
        return Ok(response);
    }

    [HttpGet("hospitalizations")]
    public async Task<IActionResult> GetTutorHospitalizations(CancellationToken cancellationToken)
    {
        var tutorId = GetTutorIdFromClaims();
        var response = await _tutorService.GetTutorHospitalizationsAsync(tutorId, cancellationToken);
        return Ok(response);
    }

    [HttpGet("petshop")]
    public async Task<IActionResult> GetTutorPetshop(CancellationToken cancellationToken)
    {
        var tutorId = GetTutorIdFromClaims();
        var response = await _tutorService.GetTutorPetshopAsync(tutorId, cancellationToken);
        return Ok(response);
    }

    private Guid GetTutorIdFromClaims()
    {
        var tutorIdClaim = User.FindFirst("tutor_id")?.Value;
        return Guid.TryParse(tutorIdClaim, out var tutorId) ? tutorId : Guid.Empty;
    }
}