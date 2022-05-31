namespace CodeInTasks.Builder.Runtime.Abstractions.Interfaces.Infrastructure
{
    public interface IResultPublisher
    {
        Task PublishAsync(SolutionStatusUpdateDto solutionStatus);
    }
}
