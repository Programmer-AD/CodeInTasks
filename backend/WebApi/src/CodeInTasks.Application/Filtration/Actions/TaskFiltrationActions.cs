using CodeInTasks.Application.Abstractions.Dtos.Task;

namespace CodeInTasks.Application.Filtration.Actions
{
    internal static class TaskFiltrationActions
    {
        public static void CreatorFilter(TaskFilterDto filterDto, FiltrationPipelineResult<TaskModel> pipelineResult)
        {
            var creatorId = filterDto.CreatorId;

            if (creatorId.HasValue)
            {
                pipelineResult.AddFilter(task => task.CreatorId == creatorId.Value);
            }
        }

        public static void CategoryFilter(TaskFilterDto filterDto, FiltrationPipelineResult<TaskModel> pipelineResult)
        {
            var categories = filterDto.Categories;

            if (categories.Any())
            {
                pipelineResult.AddFilter(task => categories.Contains(task.Category));
            }
        }

        public static void RunnerFilter(TaskFilterDto filterDto, FiltrationPipelineResult<TaskModel> pipelineResult)
        {
            var runners = filterDto.Runners;

            if (runners.Any())
            {
                pipelineResult.AddFilter(task => runners.Contains(task.Runner));
            }
        }

        public static void Ordering(TaskFilterDto _, FiltrationPipelineResult<TaskModel> pipelineResult)
        {
            pipelineResult.OrderFunction = taskQueryable => taskQueryable.OrderByDescending(x => x.CreateDate);
        }
    }
}
