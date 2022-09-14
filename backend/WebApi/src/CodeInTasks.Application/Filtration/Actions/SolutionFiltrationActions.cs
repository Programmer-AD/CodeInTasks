using CodeInTasks.WebApi.Models.Solution;

namespace CodeInTasks.Application.Filtration.Actions
{
    internal static class SolutionFiltrationActions
    {
        public static void SenderFilter(SolutionFilterModel filterModel, FiltrationPipelineResult<Solution> pipelineResult)
        {
            var senderId = filterModel.SenderId;

            if (senderId.HasValue)
            {
                pipelineResult.AddFilter(solution => solution.SenderId == senderId.Value);
            }
        }

        public static void TaskFilter(SolutionFilterModel filterModel, FiltrationPipelineResult<Solution> pipelineResult)
        {
            var taskId = filterModel.TaskId;

            if (taskId.HasValue)
            {
                pipelineResult.AddFilter(solution => solution.TaskId == taskId.Value);
            }
        }

        public static void ResultsFilter(SolutionFilterModel filterModel, FiltrationPipelineResult<Solution> pipelineResult)
        {
            var results = filterModel.Results;

            if (results.Any())
            {
                pipelineResult.AddFilter(solution => results.Contains(solution.Result));
            }
        }

        public static void Ordering(SolutionFilterModel _, FiltrationPipelineResult<Solution> pipelineResult)
        {
            pipelineResult.OrderFunction = solutionQueryable => solutionQueryable.OrderByDescending(x => x.SendTime);
        }
    }
}
