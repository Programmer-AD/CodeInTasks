using CodeInTasks.Application.Dtos.Solution;
using CodeInTasks.Application.Dtos.Task;
using CodeInTasks.Application.Filtration.Actions;
using Microsoft.Extensions.DependencyInjection;

namespace CodeInTasks.Application.Filtration
{
    internal static class DependencyInjection
    {
        internal static void AddFiltration(this IServiceCollection services)
        {
            services.AddSingleton<IFiltrationPipeline<SolutionFilterDto, Solution>>(
                _ => new FiltrationPipeline<SolutionFilterDto, Solution>(SolutionFiltrationActions.Actions));

            services.AddSingleton<IFiltrationPipeline<TaskFilterDto, TaskModel>>(
                _ => new FiltrationPipeline<TaskFilterDto, TaskModel>(TaskFiltrationActions.Actions));
        }
    }
}
