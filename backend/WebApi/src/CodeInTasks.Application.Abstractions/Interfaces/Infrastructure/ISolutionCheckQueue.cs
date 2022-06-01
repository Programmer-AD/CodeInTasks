using CodeInTasks.Application.Abstractions.Dtos.Solution;

namespace CodeInTasks.Application.Abstractions.Interfaces.Infrastructure
{
    public interface ISolutionCheckQueue
    {
        Task EnqueueSolutionCheck(SolutionQueueDto solution);
    }
}
