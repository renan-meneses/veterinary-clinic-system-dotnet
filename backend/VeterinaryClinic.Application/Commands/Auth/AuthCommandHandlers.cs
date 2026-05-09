using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using VeterinaryClinic.Domain.Dtos;
using VeterinaryClinic.Domain.Entities;
using VeterinaryClinic.Domain.Contracts.Repositories;
using VeterinaryClinic.Domain.Contracts.Services;
using VeterinaryClinic.Domain.Exceptions;
using VeterinaryClinic.Domain.Common;

namespace VeterinaryClinic.Application.Commands.Auth;

public class LoginCommandHandler : IRequestHandler<LoginCommand, ApiResponse<AuthResultDto>>
{
    private readonly IAuthService _authService;
    private readonly ILogger<LoginCommandHandler> _logger;

    public LoginCommandHandler(IAuthService authService, ILogger<LoginCommandHandler> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    public async Task<ApiResponse<AuthResultDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _authService.LoginAsync(new LoginRequestDto
            {
                Email = request.Email,
                Password = request.Password
            }, cancellationToken);

            if (result.IsFailure)
            {
                return ApiResponse<AuthResultDto>.Fail(result.Error!);
            }

            return ApiResponse<AuthResultDto>.Ok(result.Value!);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login for email {Email}", request.Email);
            return ApiResponse<AuthResultDto>.Fail("An error occurred during login.");
        }
    }
}

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, ApiResponse<AuthResultDto>>
{
    private readonly IAuthService _authService;
    private readonly ILogger<RefreshTokenCommandHandler> _logger;

    public RefreshTokenCommandHandler(IAuthService authService, ILogger<RefreshTokenCommandHandler> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    public async Task<ApiResponse<AuthResultDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var result = await _authService.RefreshTokenAsync(request.RefreshToken, cancellationToken);

        if (result.IsFailure)
        {
            return ApiResponse<AuthResultDto>.Fail(result.Error!);
        }

        return ApiResponse<AuthResultDto>.Ok(result.Value!);
    }
}

public class LogoutCommandHandler : IRequestHandler<LogoutCommand, ApiResponse<bool>>
{
    private readonly IAuthService _authService;
    private readonly ILogger<LogoutCommandHandler> _logger;

    public LogoutCommandHandler(IAuthService authService, ILogger<LogoutCommandHandler> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    public async Task<ApiResponse<bool>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var result = await _authService.LogoutAsync(request.UserId, cancellationToken);

        if (result.IsFailure)
        {
            return ApiResponse<bool>.Fail(result.Error!);
        }

        return ApiResponse<bool>.Ok(true);
    }
}

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ApiResponse<bool>>
{
    private readonly IAuthService _authService;
    private readonly ILogger<ChangePasswordCommandHandler> _logger;

    public ChangePasswordCommandHandler(IAuthService authService, ILogger<ChangePasswordCommandHandler> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    public async Task<ApiResponse<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var result = await _authService.ChangePasswordAsync(request.UserId, request.CurrentPassword, request.NewPassword, cancellationToken);

        if (result.IsFailure)
        {
            return ApiResponse<bool>.Fail(result.Error!);
        }

        return ApiResponse<bool>.Ok(true);
    }
}