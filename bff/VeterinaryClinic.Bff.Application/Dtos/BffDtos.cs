namespace VeterinaryClinic.Bff.Application.Dtos;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public List<string> Errors { get; set; } = new();

    public static ApiResponse<T> Ok(T data) => new() { Success = true, Data = data };
    public static ApiResponse<T> Fail(List<string> errors) => new() { Success = false, Errors = errors };
    public static ApiResponse<T> Fail(string error) => new() { Success = false, Errors = new List<string> { error } };
}

public class PaginatedApiResponse<T> : ApiResponse<List<T>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);

    public static PaginatedApiResponse<T> Ok(IReadOnlyList<T> items, int page, int pageSize, int totalItems)
    {
        return new PaginatedApiResponse<T>
        {
            Success = true,
            Data = items.ToList(),
            Page = page,
            PageSize = pageSize,
            TotalItems = totalItems
        };
    }
}

public class AuthResultDto
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public UserDto User { get; set; } = null!;
}

public class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? RoleName { get; set; }
    public IEnumerable<string> Permissions { get; set; } = new List<string>();
    public Guid? TutorId { get; set; }
}

public class BffAuthResultDto
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public BffUserDto User { get; set; } = null!;
}

public class BffUserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? RoleName { get; set; }
    public List<string> Permissions { get; set; } = new();
    public Guid? TutorId { get; set; }
}