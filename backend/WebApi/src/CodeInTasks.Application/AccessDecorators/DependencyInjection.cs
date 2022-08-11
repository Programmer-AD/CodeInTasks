using Microsoft.Extensions.DependencyInjection;

namespace CodeInTasks.Application.AccessDecorators
{
    internal static class DependencyInjection
    {
        public static void AddAccessDecorators(this IServiceCollection services)
        {
            services.Decorate<ITaskService, TaskServiceAccessDecorator>();
            services.Decorate<ISolutionService, SolutionServiceAccessDecorator>();
        }
    }
}
