using Microsoft.Extensions.DependencyInjection;

namespace CodeInTasks.Application.Services
{
    internal static class DependencyInjection
    {
        internal static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ISolutionService, SolutionService>();
        }
    }
}
