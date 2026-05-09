using VeterinaryClinic.Bff.Application.Clients;
using VeterinaryClinic.Bff.Application.Dtos;
using VeterinaryClinic.Bff.Domain.Models;

namespace VeterinaryClinic.Bff.Application.Services;

public interface IAnimalBffService
{
    Task<ApiResponse<List<AnimalDto>>> GetAnimalsAsync(string? searchTerm, int page, int pageSize, CancellationToken cancellationToken);
    Task<ApiResponse<AnimalDto>> GetAnimalByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ApiResponse<AnimalCompleteHistoryDto>> GetAnimalCompleteHistoryAsync(Guid id, CancellationToken cancellationToken);
    Task<ApiResponse<List<AnimalDto>>> GetAnimalsByTutorAsync(Guid tutorId, CancellationToken cancellationToken);
}

public class AnimalBffService : IAnimalBffService
{
    private readonly IVeterinaryClinicApiClient _apiClient;
    private readonly ILogger<AnimalBffService> _logger;

    public AnimalBffService(IVeterinaryClinicApiClient apiClient, ILogger<AnimalBffService> logger)
    {
        _apiClient = apiClient;
        _logger = logger;
    }

    public async Task<ApiResponse<List<AnimalDto>>> GetAnimalsAsync(string? searchTerm, int page, int pageSize, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _apiClient.GetAnimalsAsync(searchTerm, page, pageSize, cancellationToken);

            if (!response.Success)
            {
                return ApiResponse<List<AnimalDto>>.Fail(response.Errors);
            }

            return ApiResponse<List<AnimalDto>>.Ok(response.Data ?? new List<AnimalDto>());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting animals");
            return ApiResponse<List<AnimalDto>>.Fail("An error occurred.");
        }
    }

    public async Task<ApiResponse<AnimalDto>> GetAnimalByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _apiClient.GetAnimalByIdAsync(id, cancellationToken);

            if (!response.Success || response.Data == null)
            {
                return ApiResponse<AnimalDto>.Fail("Animal not found.");
            }

            return ApiResponse<AnimalDto>.Ok(response.Data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting animal by id");
            return ApiResponse<AnimalDto>.Fail("An error occurred.");
        }
    }

    public async Task<ApiResponse<AnimalCompleteHistoryDto>> GetAnimalCompleteHistoryAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _apiClient.GetAnimalCompleteHistoryAsync(id, cancellationToken);

            if (!response.Success || response.Data == null)
            {
                return ApiResponse<AnimalCompleteHistoryDto>.Fail("History not found.");
            }

            return ApiResponse<AnimalCompleteHistoryDto>.Ok(response.Data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting animal complete history");
            return ApiResponse<AnimalCompleteHistoryDto>.Fail("An error occurred.");
        }
    }

    public async Task<ApiResponse<List<AnimalDto>>> GetAnimalsByTutorAsync(Guid tutorId, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _apiClient.GetAnimalsByTutorAsync(tutorId, cancellationToken);

            if (!response.Success)
            {
                return ApiResponse<List<AnimalDto>>.Fail(response.Errors);
            }

            return ApiResponse<List<AnimalDto>>.Ok(response.Data ?? new List<AnimalDto>());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting animals by tutor");
            return ApiResponse<List<AnimalDto>>.Fail("An error occurred.");
        }
    }
}