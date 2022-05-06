namespace CodeInTasks.Application.Interfaces.Infrastructure
{
    public interface ISolutionQueue
    {
        Task EnqueueSolution(TaskSolution solution);
    }
}
