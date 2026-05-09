using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Bff.Application.Services;
using VeterinaryClinic.Bff.Domain.Models;

namespace VeterinaryClinic.Bff.Api.Controllers;

[ApiController]
[Route("bff/v1/[controller]")]
[ApiVersion("1.0")]
public class AnimalsController : ControllerBase
{
    private readonly IAnimalBffService _animalService;
    private readonly ILogger<AnimalsController> _logger;

    public AnimalsController(IAnimalBffService animalService, ILogger<AnimalsController> logger)
    {
        _animalService = animalService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAnimals(
        [FromQuery] string? searchTerm,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var response = await _animalService.GetAnimalsAsync(searchTerm, page, pageSize, cancellationToken);
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAnimalById(Guid id, CancellationToken cancellationToken)
    {
        var response = await _animalService.GetAnimalByIdAsync(id, cancellationToken);

        if (!response.Success)
        {
            return NotFound(response);
        }

        return Ok(response);
    }

    [HttpGet("{id:guid}/complete-history")]
    public async Task<IActionResult> GetAnimalCompleteHistory(Guid id, CancellationToken cancellationToken)
    {
        var response = await _animalService.GetAnimalCompleteHistoryAsync(id, cancellationToken);
        return Ok(response);
    }

    [HttpGet("tutor/{tutorId:guid}")]
    public async Task<IActionResult> GetAnimalsByTutor(Guid tutorId, CancellationToken cancellationToken)
    {
        var response = await _animalService.GetAnimalsByTutorAsync(tutorId, cancellationToken);
        return Ok(response);
    }
}