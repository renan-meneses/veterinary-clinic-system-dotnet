using VeterinaryClinic.Bff.Infrastructure.Clients;
using VeterinaryClinic.Bff.Application.Dtos;
using VeterinaryClinic.Bff.Domain.Models;

namespace VeterinaryClinic.Bff.Application.Services;

public interface IAttendanceQueueService
{
    Task<ApiResponse<QueueListDto>> GetQueueAsync(CancellationToken cancellationToken);
    Task<ApiResponse<QueueListDto>> GetMonitorQueueAsync(CancellationToken cancellationToken);
    Task<ApiResponse<bool>> CallPatientAsync(Guid id, CancellationToken cancellationToken);
    Task<ApiResponse<bool>> FinishAttendanceAsync(Guid id, CancellationToken cancellationToken);
}

public interface IPetshopService
{
    Task<ApiResponse<List<PetshopAttendanceDto>>> GetAttendancesAsync(CancellationToken cancellationToken);
    Task<ApiResponse<PetshopBoardDto>> GetPetshopBoardAsync(CancellationToken cancellationToken);
    Task<ApiResponse<bool>> UpdateAttendanceStatusAsync(Guid id, Guid status, CancellationToken cancellationToken);
}

public interface ITutorPortalService
{
    Task<ApiResponse<TutorDashboardDto>> GetTutorDashboardAsync(Guid tutorId, CancellationToken cancellationToken);
    Task<ApiResponse<List<AnimalDto>>> GetTutorAnimalsAsync(Guid tutorId, CancellationToken cancellationToken);
    Task<ApiResponse<List<VaccineDto>>> GetTutorVaccinesAsync(Guid tutorId, CancellationToken cancellationToken);
    Task<ApiResponse<List<ConsultationDto>>> GetTutorConsultationsAsync(Guid tutorId, CancellationToken cancellationToken);
    Task<ApiResponse<List<HospitalizationDto>>> GetTutorHospitalizationsAsync(Guid tutorId, CancellationToken cancellationToken);
    Task<ApiResponse<List<PetshopAttendanceDto>>> GetTutorPetshopAsync(Guid tutorId, CancellationToken cancellationToken);
}