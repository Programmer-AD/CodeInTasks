using Microsoft.Extensions.DependencyInjection;

namespace CodeInTasks.Application.Services
{
    internal static class DependencyInjection
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ISolutionService, SolutionService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
