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
}