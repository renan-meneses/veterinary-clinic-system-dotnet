using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VeterinaryClinic.Domain.Dtos;
using VeterinaryClinic.Domain.Entities;
using VeterinaryClinic.Domain.Contracts.Services;
using VeterinaryClinic.Application.Commands.Auth;
using VeterinaryClinic.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace VeterinaryClinic.Application.Handlers.Auth;

public class AuthService : IAuthService
{
    private readonly UserManager<Identity.ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IRepository<RefreshToken> _refreshTokenRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        UserManager<Identity.ApplicationUser> userManager,
        ITokenService tokenService,
        IRepository<RefreshToken> refreshTokenRepository,
        IMapper mapper,
        ILogger<AuthService> logger)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _refreshTokenRepository = refreshTokenRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<AuthResultDto>> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return Result<AuthResultDto>.Failure("Invalid email or password.");
        }

        if (!await _userManager.CheckPasswordAsync(user, request.Password))
        {
            return Result<AuthResultDto>.Failure("Invalid email or password.");
        }

        if (user.Status != Domain.Enums.UserStatus.Active)
        {
            return Result<AuthResultDto>.Failure("User account is not active.");
        }

        var roles = await _userManager.GetRolesAsync(user);
        var accessToken = _tokenService.GenerateAccessToken(user.Id, user.Email, roles);
        var refreshToken = _tokenService.GenerateRefreshToken();

        var refreshTokenEntity = new RefreshToken
        {
            UserId = user.Id,
            Token = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        };

        await _refreshTokenRepository.AddAsync(refreshTokenEntity, cancellationToken);

        user.LastLoginAt = DateTime.UtcNow;
        await _userManager.UpdateAsync(user);

        var result = new AuthResultDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddHours(1),
            User = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                RoleName = roles.FirstOrDefault()
            }
        };

        _logger.LogInformation("User {Email} logged in successfully", request.Email);

        return Result<AuthResultDto>.Success(result);
    }

    public async Task<Result<AuthResultDto>> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        var tokenEntity = await _refreshTokenRepository.GetAllAsync(cancellationToken);
        var validToken = tokenEntity.FirstOrDefault(t => t.Token == refreshToken && t.ExpiresAt > DateTime.UtcNow && t.RevokedAt == null);

        if (validToken == null)
        {
            return Result<AuthResultDto>.Failure("Invalid refresh token.");
        }

        var user = await _userManager.FindByIdAsync(validToken.UserId.ToString());
        if (user == null)
        {
            return Result<AuthResultDto>.Failure("User not found.");
        }

        validToken.RevokedAt = DateTime.UtcNow;
        validToken.ReplacedByToken = _tokenService.GenerateRefreshToken();

        var roles = await _userManager.GetRolesAsync(user);
        var newAccessToken = _tokenService.GenerateAccessToken(user.Id, user.Email, roles);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        var newTokenEntity = new RefreshToken
        {
            UserId = user.Id,
            Token = newRefreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        };

        await _refreshTokenRepository.AddAsync(newTokenEntity, cancellationToken);

        return Result<AuthResultDto>.Success(new AuthResultDto
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken,
            ExpiresAt = DateTime.UtcNow.AddHours(1),
            User = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                RoleName = roles.FirstOrDefault()
            }
        });
    }

    public async Task<Result> LogoutAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var tokens = await _refreshTokenRepository.GetAllAsync(cancellationToken);
        var userTokens = tokens.Where(t => t.UserId == userId && t.RevokedAt == null);

        foreach (var token in userTokens)
        {
            token.RevokedAt = DateTime.UtcNow;
            await _refreshTokenRepository.UpdateAsync(token, cancellationToken);
        }

        _logger.LogInformation("User {UserId} logged out", userId);

        return Result.Success();
    }

    public async Task<Result> ForgotPasswordAsync(string email, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return Result.Success();
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        // TODO: Send email with reset link

        _logger.LogInformation("Password reset requested for {Email}", email);

        return Result.Success();
    }

    public async Task<Result> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            return Result<AuthResultDto>.Failure("User not found.");
        }

        var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return Result.Failure(errors);
        }

        _logger.LogInformation("Password changed for user {UserId}", userId);

        return Result.Success();
    }
}