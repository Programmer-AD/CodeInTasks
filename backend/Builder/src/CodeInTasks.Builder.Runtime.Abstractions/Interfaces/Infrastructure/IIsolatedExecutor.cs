namespace CodeInTasks.Builder.Runtime.Abstractions.Interfaces.Infrastructure
{
    public interface IIsolatedExecutor
    {
        Task BuildAsync(string filePath, RunnerType runner, string buildName, long buildTimeout);
        Task RunAsync(string buildName, RunnerType runner, long runTimeout);
        Task RemoveBuildAsync(string buildName);
    }
}
