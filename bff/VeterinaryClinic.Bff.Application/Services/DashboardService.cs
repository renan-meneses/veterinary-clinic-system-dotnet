using VeterinaryClinic.Bff.Application.Clients;
using VeterinaryClinic.Bff.Application.Dtos;
using VeterinaryClinic.Bff.Domain.Models;

namespace VeterinaryClinic.Bff.Application.Services;

public interface IDashboardService
{
    Task<ApiResponse<DashboardDto>> GetDashboardAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<ApiResponse<FinanceDashboardDto>> GetFinanceDashboardAsync(CancellationToken cancellationToken = default);
}

public class DashboardService : IDashboardService
{
    private readonly IVeterinaryClinicApiClient _apiClient;
    private readonly ILogger<DashboardService> _logger;

    public DashboardService(IVeterinaryClinicApiClient apiClient, ILogger<DashboardService> logger)
    {
        _apiClient = apiClient;
        _logger = logger;
    }

    public async Task<ApiResponse<DashboardDto>> GetDashboardAsync(Guid userId, CancellationToken cancellationToken)
    {
        try
        {
            var dashboard = new DashboardDto
            {
                TotalAnimals = 0,
                TodayConsultations = 0,
                ActiveHospitalizations = 0,
                PetshopAttendancesInProgress = 0,
                UpcomingVaccines = 0,
                TodaySalesTotal = 0,
                FinancialBalance = 0,
                WaitingPatients = 0,
                RecentSales = new List<SaleSummaryDto>(),
                UpcomingVaccineList = new List<VaccineAlertDto>()
            };

            return ApiResponse<DashboardDto>.Ok(dashboard);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting dashboard");
            return ApiResponse<DashboardDto>.Fail("An error occurred.");
        }
    }

    public async Task<ApiResponse<FinanceDashboardDto>> GetFinanceDashboardAsync(CancellationToken cancellationToken)
    {
        try
        {
            var finance = new FinanceDashboardDto
            {
                TotalEntries = 0,
                TotalExits = 0,
                Balance = 0,
                EntryExitChart = new List<ChartDataDto>(),
                CategoryChart = new List<ChartDataDto>(),
                RecentTransactions = new List<TransactionDto>(),
                AccountsPayable = 0,
                AccountsReceivable = 0
            };

            return ApiResponse<FinanceDashboardDto>.Ok(finance);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting finance dashboard");
            return ApiResponse<FinanceDashboardDto>.Fail("An error occurred.");
        }
    }
}