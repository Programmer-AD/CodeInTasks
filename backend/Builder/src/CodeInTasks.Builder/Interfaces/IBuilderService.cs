namespace CodeInTasks.Builder.Interfaces
{
    internal interface IBuilderService
    {
        Task RunAsync(CancellationToken cancellationToken);
    }
}