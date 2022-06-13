namespace CodeInTasks.Builder.Runtime.Abstractions.Interfaces.Infrastructure
{
    public interface IDockerProvider
    {
        /// <summary>
        /// Calls docker build
        /// <para>
        /// <paramref name="directoryPath"/> must refer to directory containg Dockerfile
        /// </para>
        /// </summary>
        /// <param name="directoryPath">Path of the directory that containg files to build</param>
        /// <param name="imageName">Destination image name</param>
        /// <exception cref="TimeoutException" />
        /// <exception cref="DockerBuildException" />
        Task BuildAsync(string directoryPath, string imageName, TimeSpan buildTimeout);

        /// <summary>
        /// Calls docker run
        /// <para>
        /// This method also remove container after end of run
        /// </para>
        /// </summary>
        /// <param name="imageName">Image from which container will be created</param>
        /// <exception cref="TimeoutException" />
        /// <exception cref="DockerRunException" />
        /// <returns>
        /// Results of run
        /// </returns>
        Task<DockerRunResult> RunAsync(string imageName, TimeSpan runTimeout);

        /// <summary>
        /// Calls docker image rm
        /// <para>
        /// Has not timeout because there is no ways,
        /// where it can freeze further execution
        /// </para>
        /// <para>
        /// No exception thrown even if image isn't exist
        /// </para>
        /// </summary>
        /// <param name="imageName"></param>
        Task RemoveImageAsync(string imageName);
    }
}
