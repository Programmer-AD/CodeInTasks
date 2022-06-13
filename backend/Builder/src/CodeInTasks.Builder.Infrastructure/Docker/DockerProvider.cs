namespace CodeInTasks.Builder.Infrastructure.Docker
{
    internal class DockerProvider : IDockerProvider
    {
        public DockerProvider()
        {
            //TODO: make this using Process but add some wrapper for its creation
        }

        public Task BuildAsync(string filePath, string imageName, TimeSpan buildTimeout)
        {
            throw new NotImplementedException();
        }

        public Task RunAsync(string imageName, TimeSpan runTimeout)
        {
            throw new NotImplementedException();
        }

        public Task RemoveBuildAsync(string imageName)
        {
            throw new NotImplementedException();
        }
    }
}
