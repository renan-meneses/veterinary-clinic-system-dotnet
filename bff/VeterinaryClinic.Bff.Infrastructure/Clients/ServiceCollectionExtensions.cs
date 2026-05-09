using Refit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using VeterinaryClinic.Bff.Application.Clients;

namespace VeterinaryClinic.Bff.Infrastructure.Clients;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddVeterinaryClinicApiClient(this IServiceCollection services, IConfiguration configuration)
    {
        var baseUrl = configuration.GetValue<string>("Services:VeterinaryClinicApi:BaseUrl") ?? "http://localhost:5000";

        services.AddRefitClient<IVeterinaryClinicApiClient>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(baseUrl);
            })
            .AddHttpMessageHandler<JwtTokenHandler>();

        services.AddScoped<JwtTokenHandler>();

        return services;
    }
}

public class JwtTokenHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public JwtTokenHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault();

        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = System.Net.Http.Headers.AuthenticationHeaderValue.Parse(token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}