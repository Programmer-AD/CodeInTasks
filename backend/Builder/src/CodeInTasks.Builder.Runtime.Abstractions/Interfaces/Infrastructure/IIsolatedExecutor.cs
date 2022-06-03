namespace CodeInTasks.Builder.Runtime.Abstractions.Interfaces.Infrastructure
{
    public interface IIsolatedExecutor
    {
        Task BuildAsync(string path, string instanceName, CancellationToken cancellationToken);
        Task RunAsync(string instanceName, CancellationToken cancellationToken);
        Task RemoveAsync(string instanceName);
    }
}
