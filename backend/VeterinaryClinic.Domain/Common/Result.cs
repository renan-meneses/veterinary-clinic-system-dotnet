namespace VeterinaryClinic.Domain.Common;

public abstract class Result
{
    public bool IsSuccess { get; protected set; }
    public bool IsFailure => !IsSuccess;
    public string? Error { get; protected set; }

    public static Result Success() => new SuccessResult();
    public static Result Failure(string error) => new FailureResult(error);
    public static Result<T> Success<T>(T value) => new SuccessResult<T>(value);
    public static Result<T> Failure<T>(string error) => new FailureResult<T>(error);
}

public class SuccessResult : Result
{
    public SuccessResult()
    {
        IsSuccess = true;
    }
}

public class FailureResult : Result
{
    public FailureResult(string error)
    {
        IsSuccess = false;
        Error = error;
    }
}

public class Result<T> : Result
{
    public T? Value { get; private set; }

    private SuccessResult(T value) : base()
    {
        Value = value;
    }

    private FailureResult(string error) : base()
    {
        IsSuccess = false;
        Error = error;
    }

    public static Result<T> Success(T value) => new SuccessResult(value);
    public static Result<T> Failure(string error) => new FailureResult(error);
}

public class SuccessResult<T> : Result<T>
{
    public SuccessResult(T value) : base()
    {
        IsSuccess = true;
        Value = value;
    }
}

public class FailureResult<T> : Result<T>
{
    public FailureResult(string error) : base()
    {
        IsSuccess = false;
        Error = error;
    }
}

public class PaginatedResult<T>
{
    public IReadOnlyList<T> Items { get; }
    public int Page { get; }
    public int PageSize { get; }
    public int TotalItems { get; }
    public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);
    public bool HasNextPage => Page < TotalPages;
    public bool HasPreviousPage => Page > 1;

    public PaginatedResult(IReadOnlyList<T> items, int page, int pageSize, int totalItems)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        TotalItems = totalItems;
    }
}