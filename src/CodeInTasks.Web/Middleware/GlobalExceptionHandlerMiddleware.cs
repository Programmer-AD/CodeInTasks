using CodeInTasks.Application.Abstractions.Exceptions;

namespace CodeInTasks.Web.Middleware
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlerMiddleware> logger;

        public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var response = context.Response;

            try
            {
                await next(context);
            }
            catch (EntityNotFoundException exception)
            {
                await HandleEntityNotFoundExceptionAsync(context, response, exception);
            }
            catch (SolutionAlreadyQueuedException exception)
            {
                await HandleSolutionAlreadyQueuedExceptionAsync(context, response, exception);
            }
            catch (IdentityException exception)
            {
                await HandleIdentityExceptionAsync(context, response, exception);
            }
            catch (Exception exception)
            {
                await HandleOtherException(context, response, exception);
            }
        }

        private async Task HandleEntityNotFoundExceptionAsync(HttpContext context, HttpResponse response, EntityNotFoundException exception)
        {
            LogWarning(exception, context);

            const string message = "Entity not found";

            await SetResponsesAsync(response, StatusCodes.Status404NotFound, message);
        }

        private async Task HandleSolutionAlreadyQueuedExceptionAsync(HttpContext context, HttpResponse response, SolutionAlreadyQueuedException exception)
        {
            LogWarning(exception, context);

            const string message = "Solution already queued";

            await SetResponsesAsync(response, StatusCodes.Status400BadRequest, message);
        }

        private async Task HandleIdentityExceptionAsync(HttpContext context, HttpResponse response, IdentityException exception)
        {
            LogWarning(exception, context);

            const string message = "Identity error occured";

            await SetResponsesAsync(response, StatusCodes.Status400BadRequest, message);
        }

        private async Task HandleOtherException(HttpContext context, HttpResponse response, Exception exception)
        {
            LogError(exception, context);

            const string message = "Internal error occured";

            await SetResponsesAsync(response, StatusCodes.Status500InternalServerError, message);
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
