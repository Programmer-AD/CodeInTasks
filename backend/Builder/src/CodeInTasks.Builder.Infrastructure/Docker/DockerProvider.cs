using CodeInTasks.Domain.Enums;

namespace CodeInTasks.Builder.Infrastructure.Docker
{
    internal class DockerProvider : IDockerProvider
    {
        private readonly HttpClient httpClient;

        public DockerProvider(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public Task BuildAsync(string filePath, RunnerType runner, string imageName, long buildTimeout)
        {
            throw new NotImplementedException();
        }

        public Task RunAsync(string imageName, long runTimeout)
        {
            throw new NotImplementedException();
        }

        public Task RemoveBuildAsync(string imageName)
        {
            throw new NotImplementedException();
        }
    }
}
