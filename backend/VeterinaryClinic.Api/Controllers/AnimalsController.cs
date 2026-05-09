using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using VeterinaryClinic.Domain.Dtos;
using VeterinaryClinic.Application.Commands.Animals;
using VeterinaryClinic.Application.Queries.Animals;

namespace VeterinaryClinic.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[ApiVersion("1.0")]
[Authorize]
public class AnimalsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AnimalsController> _logger;

    public AnimalsController(IMediator mediator, ILogger<AnimalsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAnimals(
        [FromQuery] string? searchTerm,
        [FromQuery] Guid? species,
        [FromQuery] Guid? status,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(
            new GetAnimalsQuery(searchTerm, species, status, page, pageSize),
            cancellationToken);

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAnimalById(Guid id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAnimalByIdQuery(id), cancellationToken);

        if (!response.Success)
        {
            return NotFound(response);
        }

        return Ok(response);
    }

    [HttpGet("{id:guid}/complete-history")]
    public async Task<IActionResult> GetAnimalCompleteHistory(Guid id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAnimalCompleteHistoryQuery(id), cancellationToken);

        if (!response.Success)
        {
            return NotFound(response);
        }

        return Ok(response);
    }

    [HttpGet("tutor/{tutorId:guid}")]
    public async Task<IActionResult> GetAnimalsByTutor(Guid tutorId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAnimalsByTutorQuery(tutorId), cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAnimal([FromBody] CreateAnimalRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateAnimalCommand(
            request.Name,
            request.Species,
            request.Breed,
            request.Sex,
            request.BirthDate,
            request.ApproximateAge,
            request.Weight,
            request.Color,
            request.Microchip,
            request.Observations,
            request.PhotoUrl,
            request.TutorIds);

        var response = await _mediator.Send(command, cancellationToken);

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return CreatedAtAction(nameof(GetAnimalById), new { id = response.Data!.Id }, response);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAnimal(Guid id, [FromBody] UpdateAnimalRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateAnimalCommand(
            id,
            request.Name,
            request.Species,
            request.Breed,
            request.Sex,
            request.BirthDate,
            request.ApproximateAge,
            request.Weight,
            request.Color,
            request.Microchip,
            request.Observations,
            request.PhotoUrl);

        var response = await _mediator.Send(command, cancellationToken);

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> UpdateAnimalStatus(Guid id, [FromBody] UpdateStatusRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdateAnimalStatusCommand(id, request.Status), cancellationToken);
        return Ok(response);
    }

    [HttpPost("{id:guid}/tutors/{tutorId:guid}")]
    public async Task<IActionResult> AddTutorToAnimal(Guid id, Guid tutorId, [FromBody] AddTutorRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new AddTutorToAnimalCommand(id, tutorId, request.IsOwner, request.IsPrimary), cancellationToken);

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpDelete("{id:guid}/tutors/{tutorId:guid}")]
    public async Task<IActionResult> RemoveTutorFromAnimal(Guid id, Guid tutorId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new RemoveTutorFromAnimalCommand(id, tutorId), cancellationToken);
        return Ok(response);
    }
}

public class CreateAnimalRequest
{
    public string Name { get; set; } = string.Empty;
    public Guid Species { get; set; }
    public string? Breed { get; set; }
    public Guid Sex { get; set; }
    public DateTime? BirthDate { get; set; }
    public int? ApproximateAge { get; set; }
    public decimal? Weight { get; set; }
    public string? Color { get; set; }
    public string? Microchip { get; set; }
    public string? Observations { get; set; }
    public string? PhotoUrl { get; set; }
    public List<Guid> TutorIds { get; set; } = new();
}

public class UpdateAnimalRequest
{
    public string Name { get; set; } = string.Empty;
    public Guid Species { get; set; }
    public string? Breed { get; set; }
    public Guid Sex { get; set; }
    public DateTime? BirthDate { get; set; }
    public int? ApproximateAge { get; set; }
    public decimal? Weight { get; set; }
    public string? Color { get; set; }
    public string? Microchip { get; set; }
    public string? Observations { get; set; }
    public string? PhotoUrl { get; set; }
}

public class UpdateStatusRequest
{
    public Guid Status { get; set; }
}

public class AddTutorRequest
{
    public bool IsOwner { get; set; } = true;
    public bool IsPrimary { get; set; } = true;
}