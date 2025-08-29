namespace Middlewares.Middleware1
{
    public class ApiError
    {
        public int Status { get; set; }
        public string code { get; set; }
        public string Message { get; set; }
        public string? TraceId { get; set; }
        public string? Path { get; set; }
        public Dictionary<string, object>? Meta { get; set; }
    }

    public static class ErrorCodes
    {
        public const string NotFounf = "NOT_FOUND";
        public const string Unauthorized = "NOT_FOUND";
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
        protected AppException(string? message = null, Exception? inner = null) : base(message, inner) { }
    }

    public sealed class NotFoundException : AppException
    {
        public NotFoundException(string resource, object key) : base($"{resource} with key '{key} not found'")
        {

        }
    }

    public sealed class UnauthorizedException : AppException
    {
        public UnauthorizedException() : base("unauthorized.") { }
    }

    public sealed class ForbiddenException : AppException
    {
        public ForbiddenException() : base("Forbidden") { }
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
        public ValidationException(IDictionary<string,string[]> errors):base("validation Failed"){}
    } 
}