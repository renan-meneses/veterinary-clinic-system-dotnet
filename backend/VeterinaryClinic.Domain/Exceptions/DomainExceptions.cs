namespace VeterinaryClinic.Domain.Exceptions;

public class DomainException : Exception
{
    public DomainException(string message) : base(message) { }
    public DomainException(string message, Exception innerException) : base(message, innerException) { }
}

public class NotFoundException : DomainException
{
    public NotFoundException(string entityName, object key)
        : base($"Entity \"{entityName}\" ({key}) was not found.") { }
}

public class BusinessRuleException : DomainException
{
    public BusinessRuleException(string message) : base(message) { }
}

public class UnauthorizedException : DomainException
{
    public UnauthorizedException(string message = "Unauthorized") : base(message) { }
}

public class ForbiddenException : DomainException
{
    public ForbiddenException(string message = "Forbidden") : base(message) { }
}

public class ValidationException : DomainException
{
    public IDictionary<string, string[]> Errors { get; }

    public ValidationException(IDictionary<string, string[]> errors)
        : base("One or more validation errors occurred.")
    {
        Errors = errors;
    }

    public ValidationException(string message) : base(message)
    {
        Errors = new Dictionary<string, string[]>();
    }
}