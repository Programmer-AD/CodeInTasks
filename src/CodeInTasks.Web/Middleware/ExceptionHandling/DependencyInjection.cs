using CodeInTasks.Application.Abstractions.Exceptions;

namespace CodeInTasks.Web.Middleware.ExceptionHandling
{
    public static class DependencyInjection
    {
        public static void AddExceptionHandleInfos(this IServiceCollection services)
        {
            services.AddSingleton(handleInfos);
        }

        private static readonly IEnumerable<ExceptionHandleInfo> handleInfos = new ExceptionHandleInfo[]
        {
            new (typeof(EntityNotFoundException), StatusCodes.Status404NotFound, "Entity not found"),
            new (typeof(SolutionAlreadyQueuedException), StatusCodes.Status400BadRequest, "Solution already queued"),
            new (typeof(IdentityException), StatusCodes.Status400BadRequest, "Identity error occured"),
        };
    }
}
