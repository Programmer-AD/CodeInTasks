using CodeInTasks.Application.Abstractions.Dtos.Solution;

namespace CodeInTasks.Application.Filtration.Actions
{
    internal static class SolutionFiltrationActions
    {
        public static void SenderFilter(SolutionFilterDto filterDto, FiltrationPipelineResult<Solution> pipelineResult)
        {
            var senderId = filterDto.SenderId;

            if (senderId.HasValue)
            {
                pipelineResult.AddFilter(solution => solution.SenderId == senderId.Value);
            }
        }

        public static void TaskFilter(SolutionFilterDto filterDto, FiltrationPipelineResult<Solution> pipelineResult)
        {
            var taskId = filterDto.TaskId;

            if (taskId.HasValue)
            {
                pipelineResult.AddFilter(solution => solution.TaskId == taskId.Value);
            }
        }

        public static void ResultsFilter(SolutionFilterDto filterDto, FiltrationPipelineResult<Solution> pipelineResult)
        {
            var results = filterDto.Results;

            if (results.Any())
            {
                pipelineResult.AddFilter(solution => results.Contains(solution.Result));
            }
        }

        public static void Ordering(SolutionFilterDto _, FiltrationPipelineResult<Solution> pipelineResult)
        {
            pipelineResult.OrderFunction = solutionQueryable => solutionQueryable.OrderByDescending(x => x.SendTime);
        }
    }
}
