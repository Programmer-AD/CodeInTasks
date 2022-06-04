namespace CodeInTasks.Builder.Runtime.Abstractions.Interfaces.Infrastructure
{
    public interface IIsolatedExecutor
    {
        Task BuildAsync(string path, RunnerType runner, string instanceName, CancellationToken cancellationToken);
        Task RunAsync(string instanceName, RunnerType runner, CancellationToken cancellationToken);
        Task RemoveAsync(string instanceName);
    }
}
