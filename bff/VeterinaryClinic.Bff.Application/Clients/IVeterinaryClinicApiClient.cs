using Refit;
using VeterinaryClinic.Bff.Domain.Models;
using Dtos = VeterinaryClinic.Bff.Application.Dtos;

namespace VeterinaryClinic.Bff.Application.Clients;

public interface IVeterinaryClinicApiClient
{
    [Post("/api/v1/auth/login")]
    Task<Dtos.ApiResponse<Dtos.AuthResultDto>> LoginAsync(string email, string password, CancellationToken cancellationToken = default);

    [Post("/api/v1/auth/logout")]
    Task<Dtos.ApiResponse<bool>> LogoutAsync(CancellationToken cancellationToken = default);

    [Get("/api/v1/auth/me")]
    Task<Dtos.ApiResponse<Dtos.UserDto>> GetCurrentUserAsync(CancellationToken cancellationToken = default);

    [Get("/api/v1/animals")]
    Task<Dtos.PaginatedApiResponse<AnimalDto>> GetAnimalsAsync(string? searchTerm, int page, int pageSize, CancellationToken cancellationToken = default);

    [Get("/api/v1/animals/{id}")]
    Task<Dtos.ApiResponse<AnimalDto>> GetAnimalByIdAsync(Guid id, CancellationToken cancellationToken = default);

    [Get("/api/v1/animals/{id}/complete-history")]
    Task<Dtos.ApiResponse<AnimalCompleteHistoryDto>> GetAnimalCompleteHistoryAsync(Guid id, CancellationToken cancellationToken = default);

    [Get("/api/v1/animals/tutor/{tutorId}")]
    Task<Dtos.ApiResponse<List<AnimalDto>>> GetAnimalsByTutorAsync(Guid tutorId, CancellationToken cancellationToken = default);

    [Get("/api/v1/attendance-queue")]
    Task<Dtos.ApiResponse<QueueListDto>> GetAttendanceQueueAsync(CancellationToken cancellationToken = default);

    [Patch("/api/v1/attendance-queue/{id}/call")]
    Task<Dtos.ApiResponse<bool>> CallPatientAsync(Guid id, CancellationToken cancellationToken = default);

    [Patch("/api/v1/attendance-queue/{id}/finish")]
    Task<Dtos.ApiResponse<bool>> FinishAttendanceAsync(Guid id, CancellationToken cancellationToken = default);

    [Get("/api/v1/petshop/attendances")]
    Task<Dtos.ApiResponse<List<PetshopAttendanceDto>>> GetPetshopAttendancesAsync(CancellationToken cancellationToken = default);

    [Patch("/api/v1/petshop/attendances/{id}/status")]
    Task<Dtos.ApiResponse<bool>> UpdatePetshopAttendanceStatusAsync(Guid id, Guid status, CancellationToken cancellationToken = default);

    [Get("/api/v1/finance/dashboard")]
    Task<Dtos.ApiResponse<FinanceDashboardDto>> GetFinanceDashboardAsync(CancellationToken cancellationToken = default);
}
