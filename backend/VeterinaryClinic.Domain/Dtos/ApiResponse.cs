namespace VeterinaryClinic.Domain.Dtos;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public List<string> Errors { get; set; } = new();
    public List<string> Notifications { get; set; } = new();
    public MetaDto? Meta { get; set; }

    public static ApiResponse<T> Ok(T data, List<string>? notifications = null) => new()
    {
        Success = true,
        Data = data,
        Notifications = notifications ?? new()
    };

    public static ApiResponse<T> Fail(List<string> errors) => new()
    {
        Success = false,
        Errors = errors
    };

    public static ApiResponse<T> Fail(string error) => new()
    {
        Success = false,
        Errors = new List<string> { error }
    };
}

public class PaginatedApiResponse<T> : ApiResponse<List<T>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);

    public static PaginatedApiResponse<T> Ok(
        IReadOnlyList<T> items,
        int page,
        int pageSize,
        int totalItems,
        List<string>? notifications = null) => new()
    {
        Success = true,
        Data = items.ToList(),
        Page = page,
        PageSize = pageSize,
        TotalItems = totalItems,
        Notifications = notifications ?? new()
    };
}

public class MetaDto
{
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}