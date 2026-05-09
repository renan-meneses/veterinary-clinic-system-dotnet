using MediatR;
using VeterinaryClinic.Domain.Dtos;

namespace VeterinaryClinic.Application.Commands.Auth;

public record LoginCommand(string Email, string Password) : IRequest<ApiResponse<AuthResultDto>>;

public record RefreshTokenCommand(string RefreshToken) : IRequest<ApiResponse<AuthResultDto>>;

public record LogoutCommand(Guid UserId) : IRequest<ApiResponse<bool>>;

public record ForgotPasswordCommand(string Email) : IRequest<ApiResponse<bool>>;

public record ChangePasswordCommand(Guid UserId, string CurrentPassword, string NewPassword) : IRequest<ApiResponse<bool>>;

public record GetCurrentUserCommand(Guid UserId) : IRequest<ApiResponse<UserDto>>;