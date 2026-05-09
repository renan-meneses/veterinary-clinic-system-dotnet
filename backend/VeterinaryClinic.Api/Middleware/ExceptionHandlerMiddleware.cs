using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace VeterinaryClinic.Api.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "An unhandled exception occurred");

        var response = context.Response;
        response.ContentType = "application/json";

        var (statusCode, message) = exception switch
        {
            Domain.Exceptions.NotFoundException => (StatusCodes.Status404NotFound, exception.Message),
            Domain.Exceptions.UnauthorizedException => (StatusCodes.Status401Unauthorized, exception.Message),
            Domain.Exceptions.ForbiddenException => (StatusCodes.Status403Forbidden, exception.Message),
            Domain.Exceptions.ValidationException validationEx => (StatusCodes.Status400BadRequest, validationEx.Message),
            Domain.Exceptions.BusinessRuleException => (StatusCodes.Status400BadRequest, exception.Message),
            _ => (StatusCodes.Status500InternalServerError, "An internal server error occurred")
        };

        response.StatusCode = (int)statusCode;

        await response.WriteAsJsonAsync(new
        {
            success = false,
            errors = new[] { message },
            data = (object?)null
        });
    }
}

public static class ExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}