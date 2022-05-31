namespace CodeInTasks.Builder.Runtime.Abstractions.Interfaces
{
    public interface IRuntime
    {
        Task<SolutionStatusUpdateDto> HandleAsync(SolutionCheckQueueMessage checkQueueMessage);
    }
}
