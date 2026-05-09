using MediatR;
using VeterinaryClinic.Domain.Dtos;
using VeterinaryClinic.Domain.Common;

namespace VeterinaryClinic.Application.Queries.Animals;

public record GetAnimalsQuery(
    string? SearchTerm,
    Guid? Species,
    Guid? Status,
    int Page,
    int PageSize
) : IRequest<PaginatedApiResponse<AnimalDto>>;

public record GetAnimalByIdQuery(Guid Id) : IRequest<ApiResponse<AnimalDto>>;

public record GetAnimalCompleteHistoryQuery(Guid AnimalId) : IRequest<ApiResponse<AnimalCompleteHistoryDto>>;

public record GetAnimalsByTutorQuery(Guid TutorId) : IRequest<ApiResponse<List<AnimalDto>>>;

public class AnimalCompleteHistoryDto
{
    public AnimalDto Animal { get; set; } = null!;
    public List<VaccineDto> Vaccines { get; set; } = new();
    public List<ConsultationDto> Consultations { get; set; } = new();
    public List<HospitalizationDto> Hospitalizations { get; set; } = new();
    public List<MedicalRecordDto> MedicalRecords { get; set; } = new();
    public List<PetshopAttendanceDto> PetshopAttendances { get; set; } = new();
}

public class VaccineDto
{
    public Guid Id { get; set; }
    public string VaccineName { get; set; } = string.Empty;
    public DateTime ApplicationDate { get; set; }
    public DateTime? NextDoseDate { get; set; }
    public string? Batch { get; set; }
    public string? Manufacturer { get; set; }
    public Guid Status { get; set; }
}

public class ConsultationDto
{
    public Guid Id { get; set; }
    public DateTime ScheduledAt { get; set; }
    public string? Reason { get; set; }
    public string? Diagnosis { get; set; }
    public Guid Status { get; set; }
    public string? VeterinarianName { get; set; }
}

public class HospitalizationDto
{
    public Guid Id { get; set; }
    public DateTime EntryDate { get; set; }
    public DateTime? ExpectedDischargeDate { get; set; }
    public DateTime? ActualDischargeDate { get; set; }
    public string? Reason { get; set; }
    public Guid Status { get; set; }
}

public class MedicalRecordDto
{
    public Guid Id { get; set; }
    public DateTime RecordDate { get; set; }
    public string? Anamnesis { get; set; }
    public string? Diagnosis { get; set; }
    public string? Prescription { get; set; }
}

public class PetshopAttendanceDto
{
    public Guid Id { get; set; }
    public string ServiceName { get; set; } = string.Empty;
    public DateTime EntryTime { get; set; }
    public DateTime? ActualDeliveryTime { get; set; }
    public Guid Status { get; set; }
}