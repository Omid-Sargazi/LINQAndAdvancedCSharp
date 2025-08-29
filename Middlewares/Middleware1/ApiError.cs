namespace Middlewares.Middleware1
{
    public class ApiError
    {
        public int Status { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public string? TraceId { get; set; }
        public string? Path { get; set; }
        public Dictionary<string, object>? Meta { get; set; }
    }

    public static class ErrorCodes
    {
        public const string NotFound = "NOT_FOUND";
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
        public ValidationException(IDictionary<string, string[]> errors) : base("validation Failed") { }
    }


    public class ExceptionsHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionsHandlingMiddleware> _logger;
        public ExceptionsHandlingMiddleware(RequestDelegate next, ILogger<ExceptionsHandlingMiddleware> logger)
        {
            _logger = logger;
            _next = next;
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

        public async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            ApiError error;

            switch (exception)
            {
                case NotFoundException nf:
                    error = new ApiError
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Code = ErrorCodes.NotFound,
                        Message = nf.Message,
                        TraceId = context.TraceIdentifier,
                        Path = context.Request.Path,
                    };
                    break;
                case UnauthorizedAccessException:
                    error = new ApiError
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Code = ErrorCodes.Unauthorized,
                        Message = "Unauthorized",
                        TraceId = context.TraceIdentifier,
                        Path = context.Request.Path,
                    };
                    break;

                case ForbiddenException:
                    error = new ApiError
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Code = ErrorCodes.Forbidden,
                        Message = "Forbidden",
                        TraceId = context.TraceIdentifier,
                        Path = context.Request.Path,
                    };
                    break;

                case ConflictException cex:
                    error = new ApiError
                    {
                        Status = StatusCodes.Status409Conflict,
                        Code = ErrorCodes.Conflict,
                        Message = cex.Message,
                        TraceId = context.TraceIdentifier,
                        Path = context.Request.Path,
                    };
                    break;

                case BusinessRuleException bre:
                    error = new ApiError
                    {
                        Status = StatusCodes.Status422UnprocessableEntity,
                        Code = ErrorCodes.BusinessRule,
                        Message = bre.Message,
                        TraceId = context.TraceIdentifier,
                        Path = context.Request.Path
                    };
                    break;

                case ValidationException vex:
                    error = new ApiError
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Code = ErrorCodes.ValidationFailed,
                        Message = vex.Message,
                        TraceId = context.TraceIdentifier,
                        Path = context.Request.Path,
                        Meta = new Dictionary<string, object>
                        {
                            ["erorrs"] = vex.Errors
                        }
                    };
                    break;

                default:
                    error = new ApiError
                    {
                        Status = StatusCodes.Status500InternalServerError,
                        Code = ErrorCodes.Unauthorized,
                        Message = "An unexpected error occurred.",
                        TraceId = context.TraceIdentifier,
                        Path = context.Request.Path,

                    };
                    _logger.LogError(exception, "Unhandled exception occurred");
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = error.Status;

            await context.Response.WriteAsJsonAsync(error);
        }
    }
}