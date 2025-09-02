namespace ExceptionsAndMiddlewares.ManageExceptions
{
    public sealed class ApiError
    {
        public int Status { get; init; }
        public string Code { get; init; } = default!;
        public string Message { get; init; } = default!;
        public string? TraceId { get; init; }
        public string? Path { get; init; }
        public IDictionary<string, object>? Meta { get; init; }
    }

    public static class ErrorCodes
    {
        public const string NotFound = "NOT_FOUND";
        public const string Unauthorized = "UNAUTHORIZED";
        public const string Forbidden = "FORBIDDEN";
        public const string ValidationFailed = "VALIDATION_FAILED";
        public const string Conflict = "CONFLICT";
        public const string BusinessRule = "BUSINESS_RULE_VIOLATION";
        public const string TooManyRequests = "TOO_MANY_REQUESTS";
        public const string ClientCancelled = "CLIENT_CANCELLED";
        public const string Unhandled = "UNHANDLED_ERROR";
    }

    public abstract class AppException : Exception
    {
        protected AppException(string message = null, Exception? inner = null) : base(message, inner) { }
    }

    public sealed class NotFoundException : AppException
    {
        public NotFoundException(string resource, object key) : base($"{resource} with key '{key}' not found.") { }
    }

    public sealed class UnauthorizedException : AppException
    {
        public UnauthorizedException() : base("unauthorized") { }
    }

    public sealed class ForbiddenException : AppException
    {
        public ForbiddenException() : base("forbidden") { }
    }

    public sealed class ConflictException : AppException
    {
        public ConflictException(string reason) : base(reason) { }
    }

    public sealed class BusinessRuleException : AppException
    {
        public BusinessRuleException(string rule, string? details = null) : base(details is null ? rule : $"{rule}:{details}") { }
    }

    public sealed class ValidationException : AppException
    {
        public IDictionary<string, string[]> Errors { get; }
        public ValidationException(IDictionary<string, string[]> errors) : base("Validation failed.")
        {
            Errors = errors;
        }
    }
}