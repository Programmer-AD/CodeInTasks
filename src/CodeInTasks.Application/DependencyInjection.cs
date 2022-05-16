using CodeInTasks.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeInTasks.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
        {
            services.AddServices();
            services.AddFiltration();

            return services;
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ISolutionService, SolutionService>();
        }

        private static void AddFiltration(this IServiceCollection services)
        {
            //TODO: add filtration to DI
        }
    }
}
