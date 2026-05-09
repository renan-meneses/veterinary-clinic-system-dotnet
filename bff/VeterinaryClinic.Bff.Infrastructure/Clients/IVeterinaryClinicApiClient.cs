using Refit;
using VeterinaryClinic.Bff.Application.Dtos;

namespace VeterinaryClinic.Bff.Infrastructure.Clients;

public interface IVeterinaryClinicApiClient
{
    [Post("/api/v1/auth/login")]
    Task<ApiResponse<AuthResultDto>> LoginAsync(string email, string password, CancellationToken cancellationToken = default);

    [Post("/api/v1/auth/logout")]
    Task<ApiResponse<bool>> LogoutAsync(CancellationToken cancellationToken = default);

    [Get("/api/v1/auth/me")]
    Task<ApiResponse<UserDto>> GetCurrentUserAsync(CancellationToken cancellationToken = default);

    [Get("/api/v1/animals")]
    Task<PaginatedApiResponse<AnimalDto>> GetAnimalsAsync(string? searchTerm, int page, int pageSize, CancellationToken cancellationToken = default);

    [Get("/api/v1/animals/{id}")]
    Task<ApiResponse<AnimalDto>> GetAnimalByIdAsync(Guid id, CancellationToken cancellationToken = default);

    [Get("/api/v1/animals/{id}/complete-history")]
    Task<ApiResponse<AnimalCompleteHistoryDto>> GetAnimalCompleteHistoryAsync(Guid id, CancellationToken cancellationToken = default);

    [Get("/api/v1/animals/tutor/{tutorId}")]
    Task<ApiResponse<List<AnimalDto>>> GetAnimalsByTutorAsync(Guid tutorId, CancellationToken cancellationToken = default);

    [Get("/api/v1/attendance-queue")]
    Task<ApiResponse<QueueListDto>> GetAttendanceQueueAsync(CancellationToken cancellationToken = default);

    [Patch("/api/v1/attendance-queue/{id}/call")]
    Task<ApiResponse<bool>> CallPatientAsync(Guid id, CancellationToken cancellationToken = default);

    [Patch("/api/v1/attendance-queue/{id}/finish")]
    Task<ApiResponse<bool>> FinishAttendanceAsync(Guid id, CancellationToken cancellationToken = default);

    [Get("/api/v1/petshop/attendances")]
    Task<ApiResponse<List<PetshopAttendanceDto>>> GetPetshopAttendancesAsync(CancellationToken cancellationToken = default);

    [Patch("/api/v1/petshop/attendances/{id}/status")]
    Task<ApiResponse<bool>> UpdatePetshopAttendanceStatusAsync(Guid id, Guid status, CancellationToken cancellationToken = default);

    [Get("/api/v1/finance/dashboard")]
    Task<ApiResponse<FinanceDashboardDto>> GetFinanceDashboardAsync(CancellationToken cancellationToken = default);
}