namespace CodeInTasks.Builder.Runtime.Abstractions.Interfaces.Infrastructure
{
    public interface IDockerProvider
    {
        Task BuildAsync(string filePath, RunnerType runner, string imageName, long buildTimeout);
        Task RunAsync(string imageName, long runTimeout);
        Task RemoveBuildAsync(string imageName);
    }
}
