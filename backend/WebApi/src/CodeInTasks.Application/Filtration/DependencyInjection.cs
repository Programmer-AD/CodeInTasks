using CodeInTasks.Application.Abstractions.Dtos.Solution;
using CodeInTasks.Application.Abstractions.Dtos.Task;
using CodeInTasks.Application.Filtration.Actions;
using Microsoft.Extensions.DependencyInjection;

namespace CodeInTasks.Application.Filtration
{
    internal static class DependencyInjection
    {
        internal static void AddFiltration(this IServiceCollection services)
        {
            services.AddSingleton(
                _ => MakePipelineFromActionContainer<SolutionFilterDto, Solution>(typeof(SolutionFiltrationActions)));

            services.AddSingleton(
                _ => MakePipelineFromActionContainer<TaskFilterDto, TaskModel>(typeof(TaskFiltrationActions)));
        }

        private static IFiltrationPipeline<TFilterDto, TEntity> MakePipelineFromActionContainer<TFilterDto, TEntity>(Type containerType)
        {
            var actions = FiltrationActionsHelper.GetActions<TFilterDto, TEntity>(containerType);

            var pipeline = new FiltrationPipeline<TFilterDto, TEntity>(actions);

            return pipeline;
        }
    }
}
