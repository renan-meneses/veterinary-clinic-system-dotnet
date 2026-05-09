using VeterinaryClinic.Bff.Application.Clients;
using VeterinaryClinic.Bff.Application.Dtos;
using VeterinaryClinic.Bff.Domain.Models;

namespace VeterinaryClinic.Bff.Application.Services;

public interface IBffAuthService
{
    Task<ApiResponse<BffAuthResultDto>> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<bool>> LogoutAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<ApiResponse<BffUserDto>> GetCurrentUserAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<ApiResponse<NavigationMenuDto>> GetNavigationMenuAsync(Guid userId, CancellationToken cancellationToken = default);
}

public class BffAuthService : IBffAuthService
{
    private readonly IVeterinaryClinicApiClient _apiClient;
    private readonly ILogger<BffAuthService> _logger;

    public BffAuthService(IVeterinaryClinicApiClient apiClient, ILogger<BffAuthService> logger)
    {
        _apiClient = apiClient;
        _logger = logger;
    }

    public async Task<ApiResponse<BffAuthResultDto>> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _apiClient.LoginAsync(request.Email, request.Password, cancellationToken);

            if (!response.Success || response.Data == null)
            {
                return ApiResponse<BffAuthResultDto>.Fail(response.Errors);
            }

            var result = new BffAuthResultDto
            {
                AccessToken = response.Data.AccessToken,
                RefreshToken = response.Data.RefreshToken,
                ExpiresAt = response.Data.ExpiresAt,
                User = new BffUserDto
                {
                    Id = response.Data.User.Id,
                    Name = response.Data.User.Name,
                    Email = response.Data.User.Email,
                    RoleName = response.Data.User.RoleName,
                    Permissions = response.Data.User.Permissions?.ToList() ?? new List<string>(),
                    TutorId = response.Data.User.TutorId
                }
            };

            return ApiResponse<BffAuthResultDto>.Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during BFF login");
            return ApiResponse<BffAuthResultDto>.Fail("An error occurred during login.");
        }
    }

    public async Task<ApiResponse<bool>> LogoutAsync(Guid userId, CancellationToken cancellationToken)
    {
        try
        {
            await _apiClient.LogoutAsync(cancellationToken);
            return ApiResponse<bool>.Ok(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during BFF logout");
            return ApiResponse<bool>.Fail("An error occurred during logout.");
        }
    }

    public async Task<ApiResponse<BffUserDto>> GetCurrentUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _apiClient.GetCurrentUserAsync(cancellationToken);

            if (!response.Success || response.Data == null)
            {
                return ApiResponse<BffUserDto>.Fail(response.Errors);
            }

            var user = new BffUserDto
            {
                Id = response.Data.Id,
                Name = response.Data.Name,
                Email = response.Data.Email,
                RoleName = response.Data.RoleName,
                Permissions = response.Data.Permissions?.ToList() ?? new List<string>()
            };

            return ApiResponse<BffUserDto>.Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting current user");
            return ApiResponse<BffUserDto>.Fail("An error occurred.");
        }
    }

    public async Task<ApiResponse<NavigationMenuDto>> GetNavigationMenuAsync(Guid userId, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _apiClient.GetCurrentUserAsync(cancellationToken);

            if (!response.Success || response.Data == null)
            {
                return ApiResponse<NavigationMenuDto>.Fail(response.Errors);
            }

            var menu = new NavigationMenuDto
            {
                Items = BuildNavigationItems(response.Data.RoleName ?? "", response.Data.Permissions ?? new List<string>())
            };

            return ApiResponse<NavigationMenuDto>.Ok(menu);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting navigation menu");
            return ApiResponse<NavigationMenuDto>.Fail("An error occurred.");
        }
    }

    private List<NavigationItemDto> BuildNavigationItems(string roleName, List<string> permissions)
    {
        var items = new List<NavigationItemDto>();

        if (roleName == "Administrator" || permissions.Contains("dashboard:view"))
        {
            items.Add(new NavigationItemDto { Path = "/dashboard", Label = "Dashboard", Icon = "chart-bar" });
        }

        if (roleName == "Administrator" || permissions.Contains("users:view"))
        {
            items.Add(new NavigationItemDto { Path = "/users", Label = "Usuários", Icon = "users" });
        }

        if (roleName == "Administrator" || permissions.Contains("permissions:view"))
        {
            items.Add(new NavigationItemDto { Path = "/permissions", Label = "Permissões", Icon = "shield" });
        }

        if (roleName == "Administrator" || permissions.Contains("tutors:view"))
        {
            items.Add(new NavigationItemDto { Path = "/tutors", Label = "Tutores", Icon = "user" });
        }

        if (roleName == "Administrator" || permissions.Contains("animals:view"))
        {
            items.Add(new NavigationItemDto { Path = "/animals", Label = "Animais", Icon = "paw" });
        }

        if (roleName == "Administrator" || permissions.Contains("vaccines:view"))
        {
            items.Add(new NavigationItemDto { Path = "/vaccines", Label = "Vacinas", Icon = "syringe" });
        }

        if (roleName == "Administrator" || permissions.Contains("consultations:view"))
        {
            items.Add(new NavigationItemDto { Path = "/consultations", Label = "Consultas", Icon = "calendar" });
        }

        if (roleName == "Administrator" || permissions.Contains("hospitalizations:view"))
        {
            items.Add(new NavigationItemDto { Path = "/hospitalizations", Label = "Internações", Icon = "bed" });
        }

        if (roleName == "Administrator" || roleName == "Financial" || permissions.Contains("finance:view"))
        {
            items.Add(new NavigationItemDto { Path = "/finance", Label = "Financeiro", Icon = "currency" });
        }

        if (roleName == "Administrator" || permissions.Contains("products:view"))
        {
            items.Add(new NavigationItemDto { Path = "/products", Label = "Produtos", Icon = "package" });
        }

        if (roleName == "Administrator" || permissions.Contains("services:view"))
        {
            items.Add(new NavigationItemDto { Path = "/services", Label = "Serviços", Icon = "wrench" });
        }

        if (roleName == "Administrator" || permissions.Contains("sales:view"))
        {
            items.Add(new NavigationItemDto { Path = "/sales", Label = "Vendas", Icon = "shopping-cart" });
        }

        if (roleName == "Administrator" || roleName == "Veterinarian" || permissions.Contains("office:view"))
        {
            items.Add(new NavigationItemDto { Path = "/clinic-office", Label = "Consultório", Icon = "stethoscope" });
        }

        if (roleName == "Administrator" || permissions.Contains("attendance_queue:view"))
        {
            items.Add(new NavigationItemDto { Path = "/attendance-queue", Label = "Fila de Atendimento", Icon = "list" });
        }

        if (roleName == "Administrator" || permissions.Contains("clinic_structure:view"))
        {
            items.Add(new NavigationItemDto { Path = "/clinic-structure", Label = "Estrutura", Icon = "building" });
        }

        if (roleName == "Administrator" || roleName == "PetshopEmployee" || permissions.Contains("petshop:view"))
        {
            items.Add(new NavigationItemDto { Path = "/petshop", Label = "Petshop", Icon = "store" });
        }

        if (roleName == "Administrator" || permissions.Contains("reports:view"))
        {
            items.Add(new NavigationItemDto { Path = "/reports", Label = "Relatórios", Icon = "chart" });
        }

        return items;
    }
}

public class LoginRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}