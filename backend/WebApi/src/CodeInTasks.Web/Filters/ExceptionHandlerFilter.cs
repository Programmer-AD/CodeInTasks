using CodeInTasks.Web.Filters.ExceptionHandling;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CodeInTasks.Web.Filters
{
    public class ExceptionHandlerFilter : IAsyncExceptionFilter
    {
        private readonly IReadOnlyDictionary<Type, ExceptionHandleInfo> knownExceptions;
        private readonly ILogger<ExceptionHandlerFilter> logger;

        public ExceptionHandlerFilter(
            ILogger<ExceptionHandlerFilter> logger,
            IEnumerable<ExceptionHandleInfo> handleInfos)
        {
            this.logger = logger;
            knownExceptions = handleInfos.ToDictionary(x => x.ExceptionType, x => x);
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            var exception = context.Exception;
            var exceptionType = exception.GetType();

            while (exceptionType != typeof(Exception))
            {
                if (knownExceptions.TryGetValue(exceptionType, out var handleInfo))
                {
                    context.ExceptionHandled = true;
                    return HandleKnownExceptionAsync(context.HttpContext, exception, handleInfo);
                }

                exceptionType = exceptionType.BaseType;
            }

            return Task.CompletedTask;
        }

        private Task HandleKnownExceptionAsync(HttpContext context, Exception exception, ExceptionHandleInfo handleInfo)
        {
            logger.LogWarning(exception, "Requested path: {Path}", context.Request.Path);

            var response = context.Response;

            response.Clear();
            response.StatusCode = handleInfo.ResultStatusCode;
            return response.WriteAsync(handleInfo.ResultMessage);
        }
    }
}
