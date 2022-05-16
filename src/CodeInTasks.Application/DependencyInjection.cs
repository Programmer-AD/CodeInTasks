using CodeInTasks.Application.Dtos.Solution;
using CodeInTasks.Application.Dtos.Task;
using CodeInTasks.Application.Filtration;
using CodeInTasks.Application.Filtration.FiltrationActions;
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
            services.AddSingleton<IFiltrationPipeline<SolutionFilterDto, Solution>>(
                _ => new FiltrationPipeline<SolutionFilterDto, Solution>(SolutionFiltrationActions.Actions));

            services.AddSingleton<IFiltrationPipeline<TaskFilterDto, TaskModel>>(
                _ => new FiltrationPipeline<TaskFilterDto, TaskModel>(TaskFiltrationActions.Actions));
        }
    }
}
