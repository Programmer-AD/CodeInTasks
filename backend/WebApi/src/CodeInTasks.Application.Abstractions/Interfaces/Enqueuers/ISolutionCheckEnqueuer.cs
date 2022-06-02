using CodeInTasks.Application.Abstractions.Dtos.Solution;

namespace CodeInTasks.Application.Abstractions.Interfaces.Enqueuers
{
    public interface ISolutionCheckEnqueuer
    {
        Task EnqueueSolutionCheck(SolutionQueueDto solution);
    }
}
