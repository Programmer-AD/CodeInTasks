using CodeInTasks.WebApi.Models.Task;

namespace CodeInTasks.Application.Filtration.Actions
{
    internal static class TaskFiltrationActions
    {
        public static void CreatorFilter(TaskFilterModel filterModel, FiltrationPipelineResult<TaskModel> pipelineResult)
        {
            var creatorId = filterModel.CreatorId;

            if (creatorId.HasValue)
            {
                pipelineResult.AddFilter(task => task.CreatorId == creatorId.Value);
            }
        }

        public static void CategoryFilter(TaskFilterModel filterModel, FiltrationPipelineResult<TaskModel> pipelineResult)
        {
            var categories = filterModel.Categories;

            if (categories.Any())
            {
                pipelineResult.AddFilter(task => categories.Contains(task.Category));
            }
        }

        public static void RunnerFilter(TaskFilterModel filterModel, FiltrationPipelineResult<TaskModel> pipelineResult)
        {
            var runners = filterModel.Runners;

            if (runners.Any())
            {
                pipelineResult.AddFilter(task => runners.Contains(task.Runner));
            }
        }

        public static void Ordering(TaskFilterModel _, FiltrationPipelineResult<TaskModel> pipelineResult)
        {
            pipelineResult.OrderFunction = taskQueryable => taskQueryable.OrderByDescending(x => x.CreateDate);
        }
    }
}
