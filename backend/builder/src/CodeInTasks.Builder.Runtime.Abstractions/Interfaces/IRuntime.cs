namespace CodeInTasks.Builder.Runtime.Abstractions.Interfaces
{
    public interface IRuntime
    {
        Task HandleAsync(SolutionCheckQueueMessage checkQueueMessage);
    }
}
