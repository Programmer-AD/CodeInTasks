namespace CodeInTasks.Builder.Interfaces
{
    public interface IBuilderService
    {
        Task RunAsync(CancellationToken cancellationToken);
    }
}