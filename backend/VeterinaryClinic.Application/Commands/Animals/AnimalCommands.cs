using MediatR;
using VeterinaryClinic.Domain.Dtos;

namespace VeterinaryClinic.Application.Commands.Animals;

public record CreateAnimalCommand(
    string Name,
    Guid Species,
    string? Breed,
    Guid Sex,
    DateTime? BirthDate,
    int? ApproximateAge,
    decimal? Weight,
    string? Color,
    string? Microchip,
    string? Observations,
    string? PhotoUrl,
    List<Guid> TutorIds
) : IRequest<ApiResponse<AnimalDto>>;

public record UpdateAnimalCommand(
    Guid Id,
    string Name,
    Guid Species,
    string? Breed,
    Guid Sex,
    DateTime? BirthDate,
    int? ApproximateAge,
    decimal? Weight,
    string? Color,
    string? Microchip,
    string? Observations,
    string? PhotoUrl
) : IRequest<ApiResponse<AnimalDto>>;

public record UpdateAnimalStatusCommand(Guid Id, Guid Status) : IRequest<ApiResponse<bool>>;

public record AddTutorToAnimalCommand(Guid AnimalId, Guid TutorId, bool IsOwner, bool IsPrimary) : IRequest<ApiResponse<bool>>;

public record RemoveTutorFromAnimalCommand(Guid AnimalId, Guid TutorId) : IRequest<ApiResponse<bool>>;

public class AnimalDto
{
    public Guid Id { get; set; }
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
    public Guid Status { get; set; }
    public List<TutorDto> Tutors { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class TutorDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public bool IsOwner { get; set; }
    public bool IsPrimary { get; set; }
}