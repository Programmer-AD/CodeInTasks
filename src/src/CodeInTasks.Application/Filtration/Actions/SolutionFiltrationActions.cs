using CodeInTasks.Application.Abstractions.Dtos.Solution;

namespace CodeInTasks.Application.Filtration.Actions
{
    internal static class SolutionFiltrationActions
    {
        internal static readonly IEnumerable<FiltrationAction<SolutionFilterDto, Solution>> Actions
            = new FiltrationAction<SolutionFilterDto, Solution>[] { SenderFilter, TaskFilter, ResultsFilter, Ordering };

        internal static void SenderFilter(SolutionFilterDto filterDto, FiltrationPipelineResult<Solution> pipelineResult)
        {
            var senderId = filterDto.SenderId;

            pipelineResult.AddFilter(solution => solution.SenderId == senderId);
        }

        internal static void TaskFilter(SolutionFilterDto filterDto, FiltrationPipelineResult<Solution> pipelineResult)
        {
            var taskId = filterDto.TaskId;

            if (taskId.HasValue)
            {
                pipelineResult.AddFilter(solution => solution.TaskId == taskId.Value);
            }
        }

        internal static void ResultsFilter(SolutionFilterDto filterDto, FiltrationPipelineResult<Solution> pipelineResult)
        {
            var results = filterDto.Results;

            if (results.Any())
            {
                pipelineResult.AddFilter(solution => results.Contains(solution.Result));
            }
        }

        internal static void Ordering(SolutionFilterDto _, FiltrationPipelineResult<Solution> pipelineResult)
        {
            pipelineResult.OrderFunction = solutionQueryable => solutionQueryable.OrderByDescending(x => x.SendTime);
        }
    }
}
