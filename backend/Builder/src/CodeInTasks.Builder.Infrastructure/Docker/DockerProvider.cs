using CodeInTasks.Builder.Runtime.Abstractions;

namespace CodeInTasks.Builder.Infrastructure.Docker
{
    internal class DockerProvider : IDockerProvider
    {
        private readonly IProcessRunner processRunner;

        public DockerProvider(IProcessRunner processRunner)
        {
            this.processRunner = processRunner;
        }

        public async Task BuildAsync(string directoryPath, string imageName, TimeSpan buildTimeout)
        {
            var dockerArguments = $"build \"{directoryPath}\" -t {imageName} --no-cache";

            var callResult = await CallDockerAsync(dockerArguments, buildTimeout);

            if (callResult.IsKilled)
            {
                throw new TimeoutException("Build timed out");
            }
            if (!callResult.HasSuccess)
            {
                throw new DockerBuildException(callResult.StreamOutputText);
            }
        }

        public async Task<DockerRunResult> RunAsync(string imageName, TimeSpan runTimeout)
        {
            const int memoryLimit = RuntimeConstants.DockerProvider_ContainerMemoryLimitMB;
            var dockerArguments = $"run --rm --read-only --network none --memory {memoryLimit}MB {imageName}";

            var callResult = await CallDockerAsync(dockerArguments, runTimeout);

            if (callResult.IsKilled)
            {
                throw new TimeoutException("Run timed out");
            }

            var isAppErrorExitCode = callResult.ExitCode == RuntimeConstants.Docker_ApplicationErrorExitCode;

            if (!callResult.HasSuccess && !isAppErrorExitCode)
            {
                throw new DockerRunException(callResult.StreamOutputText);
            }

            var result = new DockerRunResult
            {
                HasSuccessExitCode = !isAppErrorExitCode,
                RunTime = callResult.RunTime,
                StreamOutputText = callResult.StreamOutputText
            };

            return result;
        }

        public Task RemoveImageAsync(string imageName)
        {
            var dockerArguments = $"image rm -f {imageName}";
            return CallDockerAsync(dockerArguments, Timeout.InfiniteTimeSpan);
        }

        private Task<ProcessRunnerResult> CallDockerAsync(string arguments, TimeSpan timeout)
        {
            var runnerArguments = new ProcessRunnerArguments("docker", arguments, timeout);

            return processRunner.RunProcessAsync(runnerArguments);
        }
    }
}
