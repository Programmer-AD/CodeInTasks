using CodeInTasks.Web.Middleware.ExceptionHandling;

namespace CodeInTasks.Web.Middleware
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        private readonly IReadOnlyDictionary<Type, ExceptionHandleInfo> knownExceptions;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> logger;

        public GlobalExceptionHandlerMiddleware(
            ILogger<GlobalExceptionHandlerMiddleware> logger,
            IEnumerable<ExceptionHandleInfo> handleInfos)
        {
            this.logger = logger;
            knownExceptions = handleInfos.ToDictionary(x => x.ExceptionType, x => x);
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                await HandleException(context, exception);
            }
        }

        private async Task HandleException(HttpContext context, Exception exception)
        {
            var exceptionType = exception.GetType();

            while (exceptionType != typeof(Exception))
            {
                if (knownExceptions.TryGetValue(exceptionType, out var handleInfo))
                {
                    await HandleKnownException(context, exception, handleInfo);
                    return;
                }

                exceptionType = exceptionType.BaseType;
            }

            await HandleOtherException(context, exception);
        }

        private async Task HandleKnownException(HttpContext context, Exception exception, ExceptionHandleInfo handleInfo)
        {
            LogWarning(exception, context);

            await SetResponsesAsync(context.Response, handleInfo.ResultStatusCode, handleInfo.ResultMessage);
        }

        private async Task HandleOtherException(HttpContext context, Exception exception)
        {
            LogError(exception, context);

            const string message = "Internal error occured";

            await SetResponsesAsync(context.Response, StatusCodes.Status500InternalServerError, message);
        }

        private static async Task SetResponsesAsync(HttpResponse response, int code, string message)
        {
            response.Clear();

            response.StatusCode = code;

            await response.WriteAsync(message);
        }

        private void LogWarning(Exception exception, HttpContext context)
        {
            logger.LogWarning(exception, "Requested path: {Path}", context.Request.Path);
        }

        private void LogError(Exception exception, HttpContext context)
        {
            logger.LogError(exception, "Requested path: {Path}", context.Request.Path);
        }
    }
}
