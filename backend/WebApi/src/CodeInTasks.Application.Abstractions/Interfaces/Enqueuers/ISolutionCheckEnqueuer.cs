namespace CodeInTasks.Application.Abstractions.Interfaces.Enqueuers
{
    public interface ISolutionCheckEnqueuer
    {
        Task EnqueueSolutionCheck(Solution solution);
    }
}
