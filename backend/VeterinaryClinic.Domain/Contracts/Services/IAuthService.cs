using VeterinaryClinic.Domain.Common;

namespace VeterinaryClinic.Domain.Contracts.Services;

public interface ITokenService
{
    string GenerateAccessToken(Guid userId, string email, IEnumerable<string> roles);
    string GenerateRefreshToken();
    Guid? ValidateToken(string token);
}

public interface IAuthService
{
    Task<Result<AuthResultDto>> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken = default);
    Task<Result<AuthResultDto>> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
    Task<Result> LogoutAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Result> ForgotPasswordAsync(string email, CancellationToken cancellationToken = default);
    Task<Result> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword, CancellationToken cancellationToken = default);
}

public class AuthResultDto
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public UserDto User { get; set; } = null!;
}

public class LoginRequestDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? RoleName { get; set; }
    public IEnumerable<string> Permissions { get; set; } = new List<string>();
}