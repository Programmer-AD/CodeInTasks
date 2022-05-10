using CodeInTasks.Application.Dtos.Solution;

namespace CodeInTasks.Application.Interfaces.Infrastructure
{
    public interface ISolutionCheckQueue
    {
        Task EnqueueSolution(SolutionQueueDto solution);
    }
}
