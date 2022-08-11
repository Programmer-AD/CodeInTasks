using CodeInTasks.Application.Filtration.Actions;
using CodeInTasks.WebApi.Models.Solution;
using CodeInTasks.WebApi.Models.Task;
using Microsoft.Extensions.DependencyInjection;

namespace CodeInTasks.Application.Filtration
{
    internal static class DependencyInjection
    {
        public static void AddFiltration(this IServiceCollection services)
        {
            services.AddSingleton(
                _ => MakePipelineFromActionContainer<SolutionFilterModel, Solution>(typeof(SolutionFiltrationActions)));

            services.AddSingleton(
                _ => MakePipelineFromActionContainer<TaskFilterModel, TaskModel>(typeof(TaskFiltrationActions)));
        }

        private static IFiltrationPipeline<TFilterModel, TEntity> MakePipelineFromActionContainer<TFilterModel, TEntity>(Type containerType)
        {
            var actions = FiltrationActionsHelper.GetActions<TFilterModel, TEntity>(containerType);

            var pipeline = new FiltrationPipeline<TFilterModel, TEntity>(actions);

            return pipeline;
        }
    }
}
