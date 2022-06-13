namespace CodeInTasks.Builder.Runtime.Abstractions.Interfaces.Infrastructure
{
    public interface IDockerProvider
    {
        Task BuildAsync(string filePath, string imageName, TimeSpan buildTimeout);
        Task RunAsync(string imageName, TimeSpan runTimeout);
        Task RemoveBuildAsync(string imageName);
    }
}
